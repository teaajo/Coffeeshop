using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffeeshop.Response
{
    public class KorisnikDto
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public string Tip { get; set; }

        public string Adresa { get; set; }
        public string Telefon { get; set; }
    }
}
