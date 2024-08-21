using MediatR;
using StockControl.Domain.Entities.Stock;
using StockControl.Domain.Enums;

namespace StockControl.Application.DataBase.ProductStock.Commands
{
    public class AddStockCommand : IRequest<bool>
    {
        public int ProductId { get; set; }
        public int IncomingQuantity { get; set; }
        public decimal TotalCost { get; set; }

        public static implicit operator StockMovementEntity(AddStockCommand command)
        {
            return new StockMovementEntity
            {
                ProductId = command.ProductId,
                IncomingQuantity = command.IncomingQuantity,
                TotalCost = command.TotalCost,
                MovementType = MovementType.Inbound
            };
        }
    }
}
