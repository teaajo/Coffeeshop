using AutoMapper;
using Coffeeshop.Data;
using Coffeeshop.Helpers;
using Coffeeshop.Models;
using Coffeeshop.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffeeshop.Controllers
{
    [ApiController]
    [Route("api/proizvod")]
    [Produces("application/json", "application/xml")] //Sve akcije kontrolera mogu da vraćaju definisane formate
   
    public class ProizvodController: ControllerBase
    {
       
        private readonly IProizvodRepository proizvodRepository;
        private readonly IAuthenticationHelper authenticationHelper;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public ProizvodController(IProizvodRepository proizvodRepository, IMapper mapper, IAuthenticationHelper authenticationHelper, LinkGenerator linkGenerator)
        {
            this.proizvodRepository = proizvodRepository;

            this.mapper = mapper;
            this.authenticationHelper = authenticationHelper;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Zaposleni, Kupac")]
        public ActionResult<List<ProizvodDto>> GetProizvod()
        {

            List<ProizvodDto> proizvodi = proizvodRepository.GetProizvod();
            if (proizvodi == null || proizvodi.Count == 0)
            {

                return NoContent();

            }

            return Ok(mapper.Map<List<ProizvodDto>>(proizvodi));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Zaposleni, Kupac")]
        [HttpGet("{id}")]
        public ActionResult<ProizvodConfirmation> GetProizvodId(int id)
        {

            Proizvod proizvodModel = proizvodRepository.GetProizvodById(id);
            if (proizvodModel == null)
            {

                return NotFound();
            }

            return Ok(mapper.Map<ProizvodConfirmation>(proizvodModel));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Zaposleni, Kupac")]
        [HttpGet("/tipproizvoda/{tip}")]

        public ActionResult<ProizvodDto> GetProizvodByTip(string tip)
        {

            List<Proizvod> proizvodi = proizvodRepository.GetProizvodByTip(tip);
           /* if (proizvodi == null || proizvodi.Count == 0)
            {

                return NotFound();

            }*/

            return Ok(mapper.Map<List<ProizvodDto>>(proizvodi));

        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Zaposleni")]
        [HttpDelete("{id}")]
        public IActionResult DeleteProizvod(int id)
        {
            try
            {

                Proizvod proizvodModel = proizvodRepository.GetProizvodById(id);
                if (proizvodModel == null)
                {

                    return NotFound();
                }
                proizvodRepository.DeleteProizvod(id);
                proizvodRepository.SaveChanges();

                return NoContent();
            }
            catch
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");

            }
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Zaposleni")]
        public ActionResult<ProizvodConfirmation> CreateProizvod([FromBody] ProizvodCreation proizvod)
        {
            try
            {

                Proizvod proizv = mapper.Map<Proizvod>(proizvod);
                ProizvodConfirmation confirmation = proizvodRepository.CreateProizvod(proizv);
                string location = linkGenerator.GetPathByAction("GetProizvod", "Proizvod", new { proizvodId = confirmation.Id });

                return Created(location, mapper.Map<ProizvodConfirmation>(confirmation));
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }



        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Zaposleni")]
        public ActionResult<ProizvodConfirmation> UpdateProizvod(Proizvod proizvod)
        {


            try
            {

                var oldProizvod = proizvodRepository.GetProizvodById(proizvod.Id);
                if (oldProizvod == null)
                {

                    return NotFound();
                }
                mapper.Map(proizvod, oldProizvod);
                proizvodRepository.SaveChanges();

                return Ok(mapper.Map<ProizvodConfirmation>(oldProizvod));

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");


            }


        }

        [HttpOptions]
        public IActionResult GetkorisnikOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

        


    }


}

