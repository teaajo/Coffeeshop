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
using System.Security.Claims;
using System.Threading.Tasks;

namespace Coffeeshop.Controllers
{
    [ApiController]
    [Route("api/porudzbine")]
    [Produces("application/json", "application/xml")]
    public class PorudzbinaController : ControllerBase
    {
        private readonly IPorudzbinaRepository porudzbinaRepository;
        private readonly IProizvodPorudzbinaRepository proizvodPorudzbinaRepository;

        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public PorudzbinaController(IPorudzbinaRepository porudzbinaRepository, IMapper mapper, LinkGenerator linkGenerator, IProizvodPorudzbinaRepository proizvodPorudzbinaRepository)
        {
            this.porudzbinaRepository = porudzbinaRepository;
            this.mapper = mapper;
            this.linkGenerator = linkGenerator;
            this.proizvodPorudzbinaRepository = proizvodPorudzbinaRepository;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Kupac,Zaposleni, Dostavljac")]
        public ActionResult<List<PorudzbineConfirmation>> GetPorudzbine()
        {

            List<PorudzbineConfirmation> porudzbines = mapper.Map<List<PorudzbineConfirmation>>(porudzbinaRepository.GetPorudzbine());
            if (porudzbines == null || porudzbines.Count == 0)
            {

                return NoContent();

            }

            return Ok(porudzbines);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Zaposleni, Dostavljac")]
        [HttpGet("{id}")]
        public ActionResult<Porudzbine> GetPorudzbinaById(int id)
        {

            Porudzbine porudzbinaModel = porudzbinaRepository.GetById(id);
            if (porudzbinaModel == null)
            {

                return NotFound();
            }

            return Ok(porudzbinaModel);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Kupac,Zaposleni")]
        [HttpGet("/porudzbinabykorisnik/{id}")]
        public ActionResult<List<PorudzbineConfirmation>> GetPorudzbinaByKorisnikId(int id)
        {

            List<PorudzbineConfirmation> porudzbinaModel = porudzbinaRepository.GetPorudzbinaByKorisnikId(id);
            if (porudzbinaModel == null)
            {

                return NotFound();
            }

            return Ok(porudzbinaModel);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
         [Authorize(Roles = "Zaposleni")]
        [HttpDelete("{id}")]
        public IActionResult DeletePorudzbina(int id)
        {
            try
            {

                Porudzbine porudzbinaModel = porudzbinaRepository.GetById(id);
                if (porudzbinaModel == null)
                {

                    return NotFound();
                }
                porudzbinaRepository.DeletePorudzbina(id);
                porudzbinaRepository.SaveChanges();

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
        public ActionResult<PorudzbineConfirmation> CreatePorudzbina([FromBody] PorudzbineCreation porudzbine)
        {
            
            try
            {
                foreach(var a in porudzbine.Proizvodi)
                {
                    if(a.Kolicina==0)
                    {
                        return StatusCode(StatusCodes.Status100Continue, "Proizvod nije na stanju!");
                    }
                }
                Porudzbine porudzbina = mapper.Map<Porudzbine>(porudzbine);

                PorudzbineConfirmation confirmation = porudzbinaRepository.CreatePorudzbina(porudzbina, porudzbine.Proizvodi);
                proizvodPorudzbinaRepository.CreateProizvodPorudzbina(porudzbine.Proizvodi, confirmation.Id);
                
                string location = linkGenerator.GetPathByAction("GetPorudzbine", "Porudzbina", new { tipId = confirmation.Id });

                return Created(location, mapper.Map<PorudzbineConfirmation>(confirmation));
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
        public ActionResult<PorudzbineConfirmation> UpdatePorudzbine(Porudzbine porudzbine)
        {


            try
            {

                var oldporudzbina = porudzbinaRepository.GetById(porudzbine.Id);
                if (oldporudzbina == null)
                {

                    return NotFound();
                }
                mapper.Map(porudzbine, oldporudzbina);
                porudzbinaRepository.SaveChanges();

                return Ok(mapper.Map<PorudzbineConfirmation>(oldporudzbina));

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
