using System;
using System.Collections.Generic;

#nullable disable

namespace Coffeeshop.Test
{
    public partial class Proizvod
    {
        public Proizvod()
        {
            ProizvodPorudzbines = new HashSet<ProizvodPorudzbine>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public decimal? Cena { get; set; }
        public string Opis { get; set; }
        public int? ProsecnaOcena { get; set; }
        public int? Kolicina { get; set; }
        public int? IdTip { get; set; }

        public virtual ICollection<ProizvodPorudzbine> ProizvodPorudzbines { get; set; }
    }
}
