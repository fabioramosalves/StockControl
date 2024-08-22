using MediatR;
using StockControl.Application.DataBase.ProductStock.Queries;
using StockControl.Domain.Entities;
using StockControl.Domain.Repositories;

namespace StockControl.Application.DataBase.ProductStock.Handlers
{
    public class GetSalesCustsByDayHandler : IRequestHandler<GetSalesCustsByDayQuery, IEnumerable<ProductAverageCostDto>>
    {
        private readonly IProductRepository _productRepository;
        public GetSalesCustsByDayHandler(IProductRepository productRepository) => _productRepository = productRepository;

        public async Task<IEnumerable<ProductAverageCostDto>> Handle(GetSalesCustsByDayQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetSalesCustsByDayAsync(request.MovementDate);
        }

    }
}
