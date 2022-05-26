using Coffeeshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffeeshop.Response
{
    public class ProizvodDto
    {

        public int Id { get; set; }
        public string Naziv { get; set; }
        public decimal? Cena { get; set; }
        public string Opis { get; set; }
        public int? ProsecnaOcena { get; set; }
        public int? Kolicina { get; set; }
      
        public string Tip{ get; set; }
    }
}
