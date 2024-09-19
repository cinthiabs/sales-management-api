using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class SalesDto
    {
        public int? IdProduct { get; set; }
        public int? IdClient { get; set; }
        [Required]
        public DateTime DateSale { get; set; }
        [Required]
        public string Name { get; set; } = default!;
        public string Details { get; set; } = default!;
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; } = 0.00m;
        public Situation Pay { get; set; }
    }
}
