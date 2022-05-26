using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coffeeshop.Response
{
    public class ProizvodPorudzbineConfirmation
    {
        public int? Id { get; set; }
        public int? IdPorudzbine { get; set; }
        public int? IdProizvod { get; set; }
    }
}
