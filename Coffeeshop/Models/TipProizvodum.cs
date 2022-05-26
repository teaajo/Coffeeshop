using System;
using System.Collections.Generic;

#nullable disable

namespace Coffeeshop.Models
{
    public partial class TipProizvodum
    {
        public TipProizvodum()
        {
            Proizvods = new HashSet<Proizvod>();
        }

        public int Id { get; set; }
        public string Tip { get; set; }

        public virtual ICollection<Proizvod> Proizvods { get; set; }
    }
}
