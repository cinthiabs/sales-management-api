﻿namespace Domain.Entities
{
    public class RelQuantity
    {
        public string Name { get; set; } = default!;
        public int Quantity { get;set; }
        public decimal Price { get; set; }
        public int Pay { get; set; }
    }
    public class RelPriceCost 
    {
        public string Name { get; set; } = default!;
        public decimal TotalPrice { get; set; }
    }

}
