using MediatR;
using StockControl.Application.DataBase.ProductStock.Commands;
using StockControl.Domain.Entities.Stock;
using StockControl.Domain.Repositories;

namespace StockControl.Application.DataBase.ProductStock.Handlers
{
    public class AddStockCommandHandler : IRequestHandler<AddStockCommand, bool>
    {
        private readonly IProductRepository _produtoRepository;
        public AddStockCommandHandler(IProductRepository produtoRepository) => _produtoRepository = produtoRepository;


        public async Task<bool> Handle(AddStockCommand request, CancellationToken cancellationToken)
        {
            var product = (StockMovementEntity)request;

            return await _produtoRepository.AddStockAsync(product);
        }
    }

}
