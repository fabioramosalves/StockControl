using MediatR;
using StockControl.Application.DataBase.ProductStock.Commands;
using StockControl.Domain.Repositories;

namespace StockControl.Application.DataBase.ProductStock.Handlers
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, int>
    {
        private readonly IProductRepository _productRepository;
        public DeleteProductHandler(IProductRepository productRepository) => _productRepository = productRepository;

        public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var stockProducts = await _productRepository.GetByIdAsync(request.Id);

            if (stockProducts == null || stockProducts.StockQuantity > 0) return 0;

            return await _productRepository.DeleteAsync(request.Id);
        }
    }
}
