using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class CostsDto
    {
        [Required]
        public string Quantity { get; set; } = default!;
        [Required]
        public string Name { get; set; } = default!;
        [Required]
        public DateTime DateCost { get; set; }
        [Required]
        public decimal UnitPrice { get; set; } = 0.00m;
        [Required]
        public decimal TotalPrice { get; set; } = 0.00m;
        public DateTime DateCreate { get; set; }
        public DateTime? DateEdit { get; set; }
    }
}
