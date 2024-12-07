namespace Domain.Entities
{
    public class RelClients
    {
        public string ProductName { get; set; } = default!;
        public string ClientName { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool Pay { get; set; }
        public DateTime DateSale { get; set; }
    }
}
