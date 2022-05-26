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
    [Route("api/proizvodporudzbina")]
    [Produces("application/json", "application/xml")]
    public class ProizvodPorudzbinaController : ControllerBase
    {

        private readonly IProizvodPorudzbinaRepository proizvodPorudzbinaRepository;
        private readonly IAuthenticationHelper authenticationHelper;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public ProizvodPorudzbinaController(IProizvodPorudzbinaRepository proizvodPorudzbinaRepository, IMapper mapper, IAuthenticationHelper authenticationHelper, LinkGenerator linkGenerator)
        {
            this.proizvodPorudzbinaRepository = proizvodPorudzbinaRepository;

            this.mapper = mapper;
            this.authenticationHelper = authenticationHelper;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Zaposleni, Kupac")]
        public ActionResult<List<ProizvodPorudzbineDto>> GetProizvodPorudzbina()
        {

            List<ProizvodPorudzbineDto> proizvodiporudzbine = proizvodPorudzbinaRepository.GetProizvodPorudzbina();
            if (proizvodiporudzbine == null || proizvodiporudzbine.Count == 0)
            {

                return NoContent();

            }

            return Ok(mapper.Map<List<ProizvodPorudzbineDto>>(proizvodiporudzbine));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
       [Authorize(Roles = "Zaposleni, Kupac")]
        [HttpGet("{id}")]
        public ActionResult<ProizvodPorudzbineConfirmation> GetProizvodPorudzbineId(int id)
        {

            ProizvodPorudzbine proizvodporudzbineModel = proizvodPorudzbinaRepository.GetById(id);
            if (proizvodporudzbineModel == null)
            {

                return NotFound();
            }

            return Ok(mapper.Map<ProizvodPorudzbineConfirmation>(proizvodporudzbineModel));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Zaposleni, Kupac")]
        [HttpGet("/porudzbina/{id}")]

        public ActionResult<ProizvodPorudzbineDto> GetProizvodByPorudzbina(int id)
        {

            List<ProizvodPorudzbineDto> proizvodiPorudzbine = proizvodPorudzbinaRepository.GetProizvodByPorudzbina(id);
            if (proizvodiPorudzbine == null || proizvodiPorudzbine.Count == 0)
            {

                return NotFound();

            }

            return Ok(mapper.Map<List<ProizvodPorudzbineDto>>(proizvodiPorudzbine));

        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Zaposleni")]
        [HttpDelete("{id}")]
        public IActionResult DeleteProizvodPorudzbina(int id)
        {
            try
            {

                ProizvodPorudzbine proizvodporudzbineModel = proizvodPorudzbinaRepository.GetById(id);
                if (proizvodporudzbineModel == null)
                {

                    return NotFound();
                }
                proizvodPorudzbinaRepository.DeleteProizvodPorudzbina(id);
                proizvodPorudzbinaRepository.SaveChanges();

                return NoContent();
            }
            catch
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");

            }
        }
        /*
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Zaposleni, Kupac")]
        public ActionResult<ProizvodPorudzbineConfirmation> CreateProizvod([FromBody] ProizvodPorudzbineCreation proizvodporudzbine)
        {
            try
            {

                ProizvodPorudzbine proizv = mapper.Map<ProizvodPorudzbine>(proizvodporudzbine);
                ProizvodPorudzbineConfirmation confirmation = proizvodPorudzbinaRepository.CreateProizvodPorudzbina(proizv);
                string location = linkGenerator.GetPathByAction("GetProizvodPorudzbina", "ProizvodPorudzbina", new { id = confirmation.Id});

                return Created(location, mapper.Map<ProizvodPorudzbineConfirmation>(confirmation));
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }



        }
        */

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Zaposleni, Kupac")]
        public ActionResult<ProizvodPorudzbineConfirmation> UpdateProizvod(ProizvodPorudzbine proizvodporudzbine)
        {


            try
            {

                var old= proizvodPorudzbinaRepository.GetById(proizvodporudzbine.Id);
                if (old == null)
                {

                    return NotFound();
                }
                mapper.Map(proizvodporudzbine, old);
                proizvodPorudzbinaRepository.SaveChanges();

                return Ok(mapper.Map<ProizvodPorudzbineConfirmation>(old));

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
