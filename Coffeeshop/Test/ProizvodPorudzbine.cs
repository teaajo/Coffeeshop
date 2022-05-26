using System;
using System.Collections.Generic;

#nullable disable

namespace Coffeeshop.Test
{
    public partial class ProizvodPorudzbine
    {
        public int Id { get; set; }
        public int IdPorudzbine { get; set; }
        public int IdProizvod { get; set; }

        public virtual Porudzbine IdPorudzbineNavigation { get; set; }
        public virtual Proizvod IdProizvodNavigation { get; set; }
    }
}
