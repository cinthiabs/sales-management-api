using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.Dtos
{
    public class ProductsDto
    {
        [JsonIgnore]
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; } = default!;
        public string Details { get; set; } = default!;
        [Required]
        public bool Active { get; set; } = true;
        [Required]
        public decimal Price { get; set; } = 0.00m;
        public DateTime DateCreate { get; set; } = DateTime.Now;
        public DateTime? DateEdit { get; set; }
    }
}
