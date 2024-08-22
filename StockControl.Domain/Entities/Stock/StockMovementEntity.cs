using StockControl.Domain.Enums;

namespace StockControl.Domain.Entities.Stock
{
    public class StockMovementEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int StockQuantity { get; set; }      
        public MovementType MovementType { get; set; }
        public DateTime MovementDate { get; set; }
    }
}
