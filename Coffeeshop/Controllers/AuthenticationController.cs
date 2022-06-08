using Coffeeshop.Helpers;
using Coffeeshop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Coffeeshop.Controllers
{
    [ApiController]
    [Route("api/korisnik")]
    [Produces("application/json", "application/xml")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationHelper authenticationHelper;

        public AuthenticationController(IAuthenticationHelper authenticationHelper)
        {
            this.authenticationHelper = authenticationHelper;
        }

        /// <summary>
        /// Sluzi za autentifikaciju korisnika
        /// </summary>
        /// <param name="principal">Model sa podacima na osnovu kojih se vrši autentifikacija</param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Authenticate(Principal principal)
        {
            //Pokušaj autentifikacije
            string tip;
            int id;
            if (authenticationHelper.AuthenticatePrincipal(principal))
            {
                string tokenString = authenticationHelper.GenerateJwt(principal,out tip, out id);

                return Ok(new Uloga(){ token = tokenString,uloga=tip, id=id });
            }

            //Ukoliko autentifikacija nije uspela vraća se status 401
            return Unauthorized();
        }
    }
}
