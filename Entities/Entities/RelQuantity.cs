using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class RelQuantity
    {
        public string Name { get; set; } = default!;
        public int Quantity { get;set; }
        public decimal Price { get; set; }
    }
}
