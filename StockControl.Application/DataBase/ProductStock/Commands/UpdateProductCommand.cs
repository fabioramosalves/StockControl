using MediatR;
using StockControl.Domain.Entities.Product;

namespace StockControl.Application.DataBase.ProductStock.Commands
{
    public class UpdateProductCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int StockQuantity { get; set; }

        public static implicit operator ProductEntity(UpdateProductCommand command)
        {
            return new ProductEntity
            {
                Id = command.Id,
                StockQuantity = command.StockQuantity
            };
        }
    }
}
