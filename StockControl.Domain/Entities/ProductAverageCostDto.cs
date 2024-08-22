namespace StockControl.Domain.Entities
{
    public class ProductAverageCostDto
    {
        public string ProductName { get; set; }
        public string PartNumber { get; set; }
        public decimal AverageCostPerUnit { get; set; }
        public decimal TotalCost { get; set; }
        public int TotalQuantity { get; set; }
    }
}
