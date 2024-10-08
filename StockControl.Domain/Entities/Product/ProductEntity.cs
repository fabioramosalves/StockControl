﻿namespace StockControl.Domain.Entities.Product
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        public decimal Cost { get; set; }
    }
}
