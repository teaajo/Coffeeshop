using Coffeeshop.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Coffeeshop.Models
{
    public partial class Porudzbine
    {
        public int Id { get; set; }
        public decimal? Iznos { get; set; }
        public string Adresa { get; set; }
        public DateTime? Datum { get; set; }
        public string Status { get; set; }
        public string Kupon { get; set; }

        [ForeignKey("IdKorisnik")]
        public int? IdKorisnik { get; set; }
      

        public virtual ICollection<ProizvodPorudzbine> ProizvodPorudzbines { get; set; }
    }
}
