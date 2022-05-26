using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffeeshop.Response
{
    public class PorudzbineCreation
    {
     
        public decimal? Iznos { get; set; }
        public string Adresa { get; set; }
        public DateTime? Datum { get; set; }
        public string Status { get; set; }
        public string Kupon { get; set; }

        public List<ProizvodConfirmation> Proizvodi { get; set; }


        public int? IdKorisnik { get; set; }
    }
}
