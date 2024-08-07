using Domain.Enums;

namespace Domain.Entities
{
    public class Sales
    {
        public int Id { get; set; }
        public int IdProduct { get; set; }
        public int? IdClient { get; set; }
        public DateTime DateSale { get; set; } 
        public string Name { get; set; } = default!;
        public string Details { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Situation Pay { get; set; }
        public DateTime DateCreate { get; set; } = DateTime.Now;
        public DateTime? DateEdit { get; set; }
    }
}
