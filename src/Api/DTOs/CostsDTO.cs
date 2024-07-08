using System.ComponentModel.DataAnnotations;

namespace Api.DTOs
{
    public class CostsDTO
    {
        public int Id { get; set; }
        [Required]
        public string Quantity { get; set; } = default!;
        [Required]
        public string Name { get; set; } = default!;
        [Required]
        public DateTime DateCost { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateEdit { get; set; }
    }
}
