using System;
using System.Collections.Generic;

#nullable disable

namespace Coffeeshop.Models
{
    public partial class KorisnikSistema
    {
        public KorisnikSistema()
        {
            Ocenas = new HashSet<Ocena>();
        }

        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string Tip { get; set; }
   
        public string Adresa { get; set; }
        public string Telefon { get; set; }
      public string Salt { get; set; }
        public virtual ICollection<Ocena> Ocenas { get; set; }
    }
}
