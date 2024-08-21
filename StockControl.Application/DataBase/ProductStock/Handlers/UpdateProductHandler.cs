using MediatR;
using StockControl.Application.DataBase.ProductStock.Commands;
using StockControl.Domain.Entities.Product;
using StockControl.Domain.Repositories;

namespace StockControl.Application.DataBase.ProductStock.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, int>
    {
        private readonly IProductRepository _produtoRepository;
        public UpdateProductHandler(IProductRepository produtoRepository) => _produtoRepository = produtoRepository;

        public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productQuantity = await _produtoRepository.GetByIdAsync(request.Id);

            if(productQuantity.StockQuantity < request.StockQuantity)
            {
                throw new Exception("Stock unavailable or insufficient");
            }

            var product = (ProductEntity)request;

            return await _produtoRepository.UpdateAsync(product);
        }
    }
}
