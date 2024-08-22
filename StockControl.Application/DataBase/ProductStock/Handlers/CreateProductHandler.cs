using MediatR;
using StockControl.Application.DataBase.ProductStock.Commands;
using StockControl.Domain.Entities.Product;
using StockControl.Domain.Repositories;

namespace StockControl.Application.DataBase.ProductStock.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductRepository _productRepository;
        public CreateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = (ProductEntity)request;
            return await _productRepository.CreateAsync(product);
        }
    }
}
