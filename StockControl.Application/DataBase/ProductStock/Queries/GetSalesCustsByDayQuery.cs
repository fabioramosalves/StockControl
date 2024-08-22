using MediatR;
using StockControl.Domain.Entities;

namespace StockControl.Application.DataBase.ProductStock.Queries
{
    public class GetSalesCustsByDayQuery : IRequest<IEnumerable<ProductAverageCostDto>>
    {
        public DateTime MovementDate { get; set; }
    } 
}
