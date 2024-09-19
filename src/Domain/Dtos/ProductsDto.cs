using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class ProductsDto
    {
        [Required]
        public string Name { get; set; } = default!;
        public string Details { get; set; } = default!;
        [Required]
        public bool Active { get; set; } = true;
        [Required]
        public decimal Price { get; set; } = 0.00m;
    }
}
