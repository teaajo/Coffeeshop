using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Coffeeshop.Models
{
    public partial class Ocena
    {
        public int Id { get; set; }
        public int Ocena1 { get; set; }

        [ForeignKey("IdKorisnik")]
        public int IdKorisnik { get; set; }
        [ForeignKey("IdProizvod")]
        public int IdProizvod { get; set; }

       public virtual KorisnikSistema IdKorisnikNavigation { get; set; }
       public virtual Proizvod IdProizvodNavigation { get; set; }
    }
}
