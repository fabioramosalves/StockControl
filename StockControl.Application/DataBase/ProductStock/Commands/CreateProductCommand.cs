using MediatR;
using StockControl.Domain.Entities.Product;
namespace StockControl.Application.DataBase.ProductStock.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
        public string PartNumber { get; set; } = string.Empty;
        public decimal AverageCost {  get; set; }


        public static implicit operator ProductEntity(CreateProductCommand command)
        {
            return new ProductEntity
            {
                Name = command.Name,
                PartNumber = command.PartNumber,
                StockQuantity = 0,
                AverageCost = command.AverageCost
            };
        }
    }
}
