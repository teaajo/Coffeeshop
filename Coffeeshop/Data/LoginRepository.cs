using Coffeeshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Coffeeshop.Data
{
    public class LoginRepository : ILoginRepository
    {
        
        private readonly it70g2018Context context;
        private readonly static int iterations = 1000;

        public LoginRepository(it70g2018Context context)
        {
            this.context = context;
        }
        public Tuple<string, string> HashPassword(string password)
        {
            var sBytes = new byte[password.Length];
            new RNGCryptoServiceProvider().GetNonZeroBytes(sBytes);
            var salt = Convert.ToBase64String(sBytes);


            var derivedBytes = new Rfc2898DeriveBytes(password, sBytes, iterations);

            return new Tuple<string, string>
            (
                Convert.ToBase64String(derivedBytes.GetBytes(256)), salt
            );
        }

        public bool UserWithCredentialsExists(string username, string password)
        {
            KorisnikSistema user = context.KorisnikSistemas.FirstOrDefault(u => u.KorisnickoIme == username);

            if (user == null)
            {
                return false;
            }

            //Ako smo našli korisnika sa tim korisničkim imenom proveravamo lozinku
            if (VerifyPassword(password, user.Lozinka, user.Salt))
            {
                return true;
            }
            return false;
        }

        public bool VerifyPassword(string password, string savedHash, string savedSalt)
        {
            var saltBytes = Convert.FromBase64String(savedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes,iterations);
            if (Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == savedHash)
            {
                return true;
            }
            return false;
        }
        
    }
}
