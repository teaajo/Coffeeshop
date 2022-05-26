using AutoMapper;
using Coffeeshop.Data;
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
    [Route("api/ocena")]
    [Produces("application/json", "application/xml")]
    public class OcenaController : ControllerBase
    {

        private readonly IOcenaRepository ocenaRepository;

        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public OcenaController(IOcenaRepository ocenaRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.ocenaRepository = ocenaRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Zaposleni")]
        public ActionResult<List<OcenaConfirmation>> GetOcena()
        {

            List<OcenaConfirmation> ocene = mapper.Map<List<OcenaConfirmation>>(ocenaRepository.GetOcena());
            if (ocene == null || ocene.Count == 0)
            {

                return NoContent();

            }

            return Ok(ocene);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Zaposleni")]
        [HttpGet("{id}")]
        public ActionResult<OcenaConfirmation> GetOcenaById(int id)
        {

            Ocena ocenaModel = ocenaRepository.GetById(id);
            if (ocenaModel == null)
            {

                return NotFound();
            }

            return Ok(mapper.Map<OcenaConfirmation>(ocenaModel));
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Zaposleni")]
        [HttpDelete("{id}")]
        public IActionResult DeleteOcena(int id)
        {
            try
            {

                Ocena ocenaModel = ocenaRepository.GetById(id);
                if (ocenaModel == null)
                {

                    return NotFound();
                }
                ocenaRepository.DeleteOcena(id);
                ocenaRepository.SaveChanges();
               
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
        [Authorize(Roles = "Zaposleni, Kupac")]

        public ActionResult<OcenaConfirmation> CreateOcena([FromBody] OcenaCreation ocena)
        {
            try
            {

                Ocena ocene = mapper.Map<Ocena>(ocena);

                OcenaConfirmation confirmation = ocenaRepository.CreateOcena(ocene);
                string location = linkGenerator.GetPathByAction("GetOcena", "Ocena", new { tipId = confirmation.Id });

                return Created(location, mapper.Map<OcenaConfirmation>(confirmation));
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
        [Authorize(Roles = "Zaposleni, Kupac")]
        public ActionResult<OcenaConfirmation> UpdateOcena(Ocena ocena)
        {


            try
            {

                var oldocena = ocenaRepository.GetById(ocena.Id);
                if (oldocena == null)
                {

                    return NotFound();
                }
                mapper.Map(ocena, oldocena);
                ocenaRepository.SaveChanges();

                return Ok(mapper.Map<OcenaConfirmation>(oldocena));

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
