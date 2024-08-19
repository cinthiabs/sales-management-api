using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class SalesDto
    {
        public int? Id { get; set; }
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
        public decimal Price { get; set; }
        public Situation Pay { get; set; }
        public DateTime DateCreate { get; set; } = DateTime.Now;
        public DateTime? DateEdit { get; set; }
    }
}
