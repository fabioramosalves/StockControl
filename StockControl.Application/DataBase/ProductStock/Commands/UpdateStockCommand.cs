using MediatR;
using StockControl.Domain.Entities;
using StockControl.Domain.Entities.Stock;
using StockControl.Domain.Enums;

namespace StockControl.Application.DataBase.ProductStock.Commands
{
    public class UpdateStockCommand : IRequest<BaseResponse>
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public MovementType MovementType { get; set; }

        public static implicit operator StockMovementEntity(UpdateStockCommand command)
        {
            return new StockMovementEntity
            {
                ProductId = command.ProductId,
                Quantity = command.Quantity,
                MovementType = command.MovementType
            };
        }
    }
}
