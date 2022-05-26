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
    [Route("api/tip")]
    [Produces("application/json", "application/xml")]
    public class TipProizvodaController : ControllerBase
    {

        private readonly ITipProizvodaRepository tipProizvodaRepository;
   
        private readonly IMapper mapper;
        private readonly LinkGenerator linkGenerator;

        public TipProizvodaController(ITipProizvodaRepository tipProizvodaRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            this.tipProizvodaRepository = tipProizvodaRepository;

            this.mapper = mapper;
          
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public ActionResult<List<TipDto>> GetTipovi()
        {

            List<TipDto> tipovi = tipProizvodaRepository.GetTip();
            if (tipovi == null || tipovi.Count == 0)
            {

                return NoContent();

            }

            return Ok(tipovi);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public ActionResult<TipDto> GetTipById(int id)
        {

            TipProizvodum tipModel = tipProizvodaRepository.GetById(id);
            if (tipModel == null)
            {

                return NotFound();
            }

            return Ok(mapper.Map<TipDto>(tipModel));
        }

       
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        // [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public IActionResult DeleteTip(int id)
        {
            try
            {

                TipProizvodum tipModel = tipProizvodaRepository.GetById(id);
                if (tipModel == null)
                {

                    return NotFound();
                }
                tipProizvodaRepository.DeleteTip(id);
                tipProizvodaRepository.SaveChanges();

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
        [AllowAnonymous]
        public ActionResult<TipDto> CreateTip([FromBody] TipProizvodum tipovi)
        {
            try
            {

                TipProizvodum tip = mapper.Map<TipProizvodum>(tipovi);

                TipDto confirmation = tipProizvodaRepository.CreateTip(tip);
                string location = linkGenerator.GetPathByAction("GetTipovi", "TipProizvoda", new { tipId = confirmation.Id });

                return Created(location, mapper.Map<TipDto>(confirmation));
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
        //[Authorize(Roles = "Administrator")]
        public ActionResult<TipDto> UpdateProizvod(TipProizvodum tip)
        {


            try
            {

                var oldtip= tipProizvodaRepository.GetById(tip.Id);
                if (oldtip == null)
                {

                    return NotFound();
                }
                mapper.Map(tip, oldtip);
                tipProizvodaRepository.SaveChanges();

                return Ok(mapper.Map<TipDto>(oldtip));

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
