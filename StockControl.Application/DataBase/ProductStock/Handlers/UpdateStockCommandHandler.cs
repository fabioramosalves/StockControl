using MediatR;
using StockControl.Application.DataBase.ProductStock.Commands;
using StockControl.Domain.Entities;
using StockControl.Domain.Entities.Stock;
using StockControl.Domain.Enums;
using StockControl.Domain.Repositories;

namespace StockControl.Application.DataBase.ProductStock.Handlers
{
    public class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand, BaseResponse>
    {
        private readonly IProductRepository _produtoRepository;
        public UpdateStockCommandHandler(IProductRepository produtoRepository) => _produtoRepository = produtoRepository;

        public async Task<BaseResponse> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var stock = (StockMovementEntity)request;

            var currencyStock = await _produtoRepository.GetByIdAsync(request.ProductId);

            if(currencyStock is null)
            {
                response.Message = "Product was not found in stock.";
                return response;
            }

            if (request.MovementType == MovementType.Outbound) 
            {
                if (currencyStock is null || currencyStock.StockQuantity < stock.Quantity)
                {
                    response.Message = "Insufficient stock to complete the operation.";
                    return response;
                }

                stock.StockQuantity = currencyStock.StockQuantity - stock.Quantity;
            }
            else
            {
                stock.StockQuantity = currencyStock.StockQuantity + stock.Quantity;
            }

            response.Success = await _produtoRepository.UpdateStockAsync(stock);

            return response;
        }
    }
}
