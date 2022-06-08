
using Coffeeshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffeeshop.Helpers
{
    public interface IAuthenticationHelper
    {
        

        public bool AuthenticatePrincipal(Principal principal);

        public string GenerateJwt(Principal principal,out string tip, out int id);
        public void CreateHash(KorisnikSistema korisnik);   
    }
}
