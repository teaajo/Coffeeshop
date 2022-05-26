using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffeeshop.Response
{
    public class OcenaConfirmation
    {
        public int Id { get; set; }
        public int Ocena1 { get; set; }

     
        public int IdKorisnik { get; set; }
        
        public int IdProizvod { get; set; }
    }
}
