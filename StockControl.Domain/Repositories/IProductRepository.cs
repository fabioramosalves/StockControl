using StockControl.Domain.Entities;
using StockControl.Domain.Entities.Product;
using StockControl.Domain.Entities.Stock;

namespace StockControl.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<int> CreateAsync(ProductEntity product);
        Task<ProductEntity> GetByIdAsync(int id);
        Task<bool> UpdateStockAsync(StockMovementEntity product);
        Task<int> DeleteAsync(int id);
        Task<IEnumerable<ProductAverageCostDto>> GetSalesCustsByDayAsync(DateTime day);
    }
}
