using System.ComponentModel.DataAnnotations;

namespace sales_management_api.DTOs
{
    public class SalesDTO
    {
        public int Id { get; set; }
        public int IdProduct { get; set; }
        public int IdClient { get; set; }
        [Required]
        public DateTime DateSale { get; set; }
        [Required]
        public string Name { get; set; } = default!;
        public string Details { get; set; } = default!;
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        public bool Pay { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateEdit { get; set; }
    }
}
