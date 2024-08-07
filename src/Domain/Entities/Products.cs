﻿namespace Domain.Entities
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Details { get; set; } = default!;
        public bool Active { get; set; } = true;
        public decimal Price { get; set; }
        public DateTime DateCreate { get; set; } = DateTime.Now;
        public DateTime? DateEdit { get; set; }
    }
}
