using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Coffeeshop.Models
{
    public partial class Proizvod
    {
        
        public Proizvod()
        {
            Ocenas = new HashSet<Ocena>();
        }

        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public decimal? Cena { get; set; }
        public string Opis { get; set; }
        public int? ProsecnaOcena { get; set; }
        public int? Kolicina { get; set; }
        public int? IdTip { get; set; }
        [ForeignKey("Id")]

     

        public virtual TipProizvodum IdTipNavigation { get; set; }
       public virtual ICollection<Ocena> Ocenas { get; set; }
       public virtual ICollection<ProizvodPorudzbine> ProizvodPorudzbines { get; set; }
    }
}
