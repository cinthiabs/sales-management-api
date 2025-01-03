﻿
namespace Domain.Entities
{
    public class ProductCost
    {
        public int ProductCostId { get; set; }
        public int IdProductTotalCost { get; set; }
        public int? IdCost { get; set; }
        public decimal TotalProductPrice { get; set; }
        public int TotalQuantity { get; set; }
        public int QuantityRequired { get; set; }
        public decimal IngredientCost { get; set; }
        public DateTime DateCreate { get; set; } = DateTime.Now;
        public DateTime? DateEdit { get; set; }
    }
    public class ProductTotalCosts
    {
        public int IdProductTotalCost { get; set; }
        public int IdProduct { get; set; }
        public string? ProductName { get; set; }
        public decimal TotalProductCost { get; set; }
        public bool Active { get; set; }
        public DateTime DateCreate { get; set; } = DateTime.Now;
        public DateTime? DateEdit { get; set; }
        public List<ProductCost> ProductCost { get; set; } = new List<ProductCost>();
    }
}
