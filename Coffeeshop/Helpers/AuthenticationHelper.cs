using Coffeeshop.Data;
using Coffeeshop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Coffeeshop.Helpers
{
    public class AuthenticationHelper : IAuthenticationHelper
    {
        
        private readonly IConfiguration configuration;
        private readonly ILoginRepository loginRepository;
        private readonly it70g2018Context context;

        public AuthenticationHelper(IConfiguration configuration, ILoginRepository loginRepository, it70g2018Context context)
        {
            this.configuration = configuration;
            this.loginRepository = loginRepository;
            this.context = context;
        }
        public bool AuthenticatePrincipal(Principal principal)
        {
            if (loginRepository.UserWithCredentialsExists(principal.KorisnickoIme, principal.Lozinka))
            {
                return true;
            }

            return false;
        }

        public void CreateHash(KorisnikSistema korisnik)
        {
            var userpass = loginRepository.HashPassword(korisnik.Lozinka);
            korisnik.Lozinka = userpass.Item1;
            korisnik.Salt = userpass.Item2;
           
        }

        public string GenerateJwt(Principal principal)
        {

            string tip = context.KorisnikSistemas.First(e => e.KorisnickoIme == principal.KorisnickoIme).Tip;
            
          // string tip = context.KorisnikSistemas.Include(t => t.Tip).First(e => e.KorisnickoIme == principal.KorisnickoIme).Tip.ToString();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                                             configuration["Jwt:Issuer"],
                                              new List<Claim>() { new Claim(ClaimTypes.Role, tip) },
                                             expires: DateTime.Now.AddMinutes(600),
                                             signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
    }
}
