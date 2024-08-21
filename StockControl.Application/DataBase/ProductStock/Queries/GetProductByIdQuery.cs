using MediatR;
using StockControl.Domain.Entities.Product;

namespace StockControl.Application.DataBase.Product.Queries
{
    public class GetProductByIdQuery : IRequest<ProductEntity>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PartNumber { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        public decimal AverageCost { get; set; }
    }
}
