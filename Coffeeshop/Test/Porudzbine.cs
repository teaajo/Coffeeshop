using System;
using System.Collections.Generic;

#nullable disable

namespace Coffeeshop.Test
{
    public partial class Porudzbine
    {
        public Porudzbine()
        {
            ProizvodPorudzbines = new HashSet<ProizvodPorudzbine>();
        }

        public int Id { get; set; }
        public decimal? Iznos { get; set; }
        public string Adresa { get; set; }
        public DateTime? Datum { get; set; }
        public string Status { get; set; }
        public string Kupon { get; set; }
        public int? IdKorisnik { get; set; }

        public virtual ICollection<ProizvodPorudzbine> ProizvodPorudzbines { get; set; }
    }
}
