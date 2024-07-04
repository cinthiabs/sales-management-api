namespace sales_management_api.DTOs
{
    public class SalesDTO
    {
        public int IdProduto { get; set; }
        public DateTime DateSale { get; set; }
        public string Name { get; set; } = default!;
        public string Details { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool Pay { get; set; }
    }
}
