namespace Domain.Entities
{
    public class Costs
    {
        public int Id { get; set; }
        public string Quantity { get; set; } = default!;
        public string Name { get; set; } = default!;
        public DateTime DateCost { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime DateCreate { get; set; } = DateTime.Now;
        public DateTime DateEdit { get; set; }
    }
}
