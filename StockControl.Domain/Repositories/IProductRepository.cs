using StockControl.Domain.Entities.Product;
using StockControl.Domain.Entities.Stock;

namespace StockControl.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<int> CreateAsync(ProductEntity product);
        Task<ProductEntity> GetByIdAsync(int id);
        Task<int> UpdateAsync(ProductEntity product);
        Task<bool> AddStockAsync(StockMovementEntity product);
        Task<int> DeleteAsync(int id);
    }
}
