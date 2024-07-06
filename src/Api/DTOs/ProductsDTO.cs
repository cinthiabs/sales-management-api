using System.ComponentModel.DataAnnotations;

namespace Api.DTOs
{
    public class ProductsDTO
    {
        [Required]
        public string Name { get; set; } = default!;
        public string Details { get; set; } = default!;
        [Required]
        public bool Active { get; set; } = true;
        [Required]
        public decimal Price { get; set; }
    }
}
