using MediatR;
using StockControl.Application.DataBase.Product.Queries;
using StockControl.Domain.Entities.Product;
using StockControl.Domain.Repositories;

namespace StockControl.Application.DataBase.ProductStock.Handlers
{ 
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductEntity>
    {
        private readonly IProductRepository _productRepository;
        public GetProductByIdHandler(IProductRepository productRepository) => _productRepository = productRepository;

        public async Task<ProductEntity> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetByIdAsync(request.Id);
        }
    }
}
