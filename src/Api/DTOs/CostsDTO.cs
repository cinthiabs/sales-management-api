using System.ComponentModel.DataAnnotations;

namespace Api.DTOs
{
    public class CostsDTO
    {
        [Required]
        public string Quantity { get; set; } = default!;
        [Required]
        public string Name { get; set; } = default!;
        [Required]
        public DateTime DateCost { get; set; }
        public decimal UnitPrice { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
    }
}
