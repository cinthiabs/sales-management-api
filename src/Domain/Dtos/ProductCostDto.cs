using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class ProductCostDto
    {
        public int Id { get; set; }
        public int? IdCost { get; set; }
        [Required]
        public decimal TotalProductPrice { get; set; }
        [Required]
        public int TotalQuantity { get; set; }
        [Required]
        public int QuantityRequired { get; set; }
        [Required]
        public decimal IngredientCost { get; set; }
    }
    public class ProductTotalCostDto
    {
        public int IdProduct { get; set; }
        [Required]
        public decimal TotalProductCost { get; set; }
        public  List<ProductCostDto> UnitCost { get; set; }
    }
}
