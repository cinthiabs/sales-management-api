using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Sales
    {
        public int Id { get; set; }
        public int IdProduto { get; set; }
        public DateTime DateSale { get; set; } 
        public string Name { get; set; } = default!;
        public string Details { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime DataCreate { get; set; } = DateTime.Now;
        public DateTime? DataEdit { get; set; }
    }
}
