using MediatR;

namespace StockControl.Application.DataBase.ProductStock.Commands
{
    public class DeleteProductCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
