using Dapper;
using Org.BouncyCastle.Asn1.Ocsp;
using StockControl.Domain.Entities;
using StockControl.Domain.Entities.Product;
using StockControl.Domain.Entities.Stock;
using StockControl.Domain.Repositories;
using System.Data;

namespace StockControl.Persistence.Repositories.Producto
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _dbConnection;

        public ProductRepository(IDbConnection dbConnection) => _dbConnection = dbConnection;

        public async Task<int> CreateAsync(ProductEntity product)
        {
            return await _dbConnection.ExecuteScalarAsync<int>(ProductSqlCommands.CreateProduct, product);
        }
        public async Task<int> DeleteAsync(int id)
        {
            return await _dbConnection.ExecuteAsync(ProductSqlCommands.DeleteProduct, new { Id = id });
        }

        public async Task<ProductEntity> GetByIdAsync(int id)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<ProductEntity>(ProductSqlCommands.GetProductById, new { Id = id });
        }

        public async Task<IEnumerable<ProductAverageCostDto>> GetSalesCustsByDayAsync(DateTime day)
        {
            var results = await _dbConnection.QueryAsync<ProductAverageCostDto>(
                                        ProductSqlCommands.GetSalesCustsByDay,new { MovementDate = day });

            return results;
        }

        public async Task<bool> UpdateStockAsync(StockMovementEntity stock)
        {
            using var transaction = _dbConnection.BeginTransaction();
            try
            {       
                await _dbConnection.ExecuteAsync(ProductSqlCommands.UpdateProduct, new
                {
                    stock.StockQuantity,
                    stock.ProductId
                }, transaction);

                await _dbConnection.ExecuteAsync(ProductSqlCommands.InsertMovementSql, new
                {
                    stock.ProductId,
                    stock.Quantity,
                    stock.MovementType
                }, transaction);

                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                transaction.Rollback();
                return false;
            }
        }
    }
}
