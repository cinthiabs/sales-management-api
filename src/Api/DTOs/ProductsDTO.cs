using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public class ProductsDto
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; } = default!;
        public string Details { get; set; } = default!;
        [Required]
        public bool Active { get; set; } = true;
        [Required]
        public decimal Price { get; set; }
        public DateTime DateCreate { get; set; } = DateTime.Now;
        public DateTime? DateEdit { get; set; }
    }
}
