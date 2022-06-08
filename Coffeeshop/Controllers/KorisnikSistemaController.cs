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
    [Route("api/korisnik")]
    [Produces("application/json", "application/xml")]
    [Authorize(Roles = "Zaposleni, Kupac")]
    public class KorisnikSistemaController : ControllerBase
    {
        private readonly IKorisnikSistemaRepository korisnikSistemaRepository;
        private readonly IAuthenticationHelper authenticationHelper;
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public KorisnikSistemaController(IKorisnikSistemaRepository korisnikSistemaRepository, IMapper mapper, IAuthenticationHelper authenticationHelper, LinkGenerator linkGenerator)
        {
            this.korisnikSistemaRepository = korisnikSistemaRepository;

            this.mapper = mapper;
            this.authenticationHelper = authenticationHelper;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Zaposleni")]

        public ActionResult<List<KorisnikDto>> GetKorisnici()
        {
          
            List<KorisnikDto> korisnici = korisnikSistemaRepository.GetKorisnik();
            if (korisnici == null || korisnici.Count == 0)
            {
               
                return NoContent();

            }
            
            return Ok(korisnici);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Zaposleni, Kupac")]

        [HttpGet("{id}")]
        public ActionResult<KorisnikDto> GetKorisnik(int id)
        {

            KorisnikSistema korisnikModel = korisnikSistemaRepository.GetKorisnikById(id);
            if (korisnikModel == null)
            {
                
                return NotFound();
            }
            
            return Ok(mapper.Map<KorisnikDto>(korisnikModel));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Zaposleni")]

        [HttpGet("/tipkorisnika/{tip}")]

        public ActionResult<KorisnikDto> GetKorisnikByTip(string tip)
        {
           
            List<KorisnikDto> korisnici = korisnikSistemaRepository.GetKorisnikByTip(tip);
            if (korisnici == null || korisnici.Count == 0)
            {
               
                return NotFound();

            }
           
            return Ok(korisnici);

        }
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Zaposleni")]
        [HttpDelete("{id}")]
        public IActionResult DeleteKorisnik(int id)
        {
            try
            {
                
                KorisnikSistema korisnikModel = korisnikSistemaRepository.GetKorisnikById(id);
                if (korisnikModel == null)
                {
                  
                    return NotFound();
                }
                korisnikSistemaRepository.DeleteKorisnik(id);
                korisnikSistemaRepository.SaveChanges();
                
                return NoContent();
            }
            catch (Exception e)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");

            }
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public ActionResult<KorisnikConfirmation> CreateKorisnik([FromBody] KorisnikCreation korisnikk)
        {
            try
            {
               
                KorisnikSistema korisnik = mapper.Map<KorisnikSistema>(korisnikk);
                authenticationHelper.CreateHash(korisnik);
                KorisnikConfirmation confirmation = korisnikSistemaRepository.CreateKorisnik(korisnik);
                string location = linkGenerator.GetPathByAction("GetKorisnici", "KorisnikSistema", new { korisnikId = confirmation.Id });
                
                return Created(location, mapper.Map<KorisnikConfirmation>(confirmation));
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
        public ActionResult<KorisnikConfirmation> UpdateKorisnik(KorisnikSistema korisnik)
        {


            try
            {
               
                var oldKorisnik = korisnikSistemaRepository.GetKorisnikById(korisnik.Id);
                if (oldKorisnik == null)
                {
                   
                    return NotFound();
                }
                mapper.Map(korisnik, oldKorisnik);

                korisnikSistemaRepository.SaveChanges();

                return Ok(mapper.Map<KorisnikConfirmation>(oldKorisnik));

            }
            catch (Exception e)
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
