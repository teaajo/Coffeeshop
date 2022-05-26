using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffeeshop.Response
{
    public class PorudzbineConfirmation
    {
        public int Id { get; set; }
        public decimal? Iznos { get; set; }
        public string Adresa { get; set; }
        public DateTime? Datum { get; set; }
        public string Status { get; set; }
        public string Kupon { get; set; }

       
        public int? IdKorisnik { get; set; }
    }
}
