namespace StockControl.Persistence.Repositories.Producto
{
    public static class ProductSqlCommands
    {
        public const string CreateProductSql = @"
        INSERT INTO Products (Name,PartNumber, StockQuantity, Cost)
        VALUES (@Name,'' , @StockQuantity, @Cost);
        SELECT LAST_INSERT_ID();";

        public const string UpdateProducartNumbertSql = @"
        UPDATE Products
        SET PartNumber = @PartNumber
        WHERE Id = @ProductId;";

        public const string InsertMovementSql = @"
        INSERT INTO StockMovements (ProductId, Quantity, MovementType, MovementDate)
        VALUES (@ProductId, @Quantity, @MovementType, NOW());";

        public const string GetProductByIdSql = @"
        SELECT Id, Name, PartNumber, StockQuantity, Cost 
        FROM Products 
        WHERE Id = @Id;";

        public const string UpdateProductSql = @"
        UPDATE Products
        SET StockQuantity = @StockQuantity
        WHERE Id = @ProductId;";

        public const string DeleteProductSql = @"
        DELETE FROM Products 
        WHERE Id = @Id;";

        public const string GetAllProductsSql = @"
        SELECT Id, Name, PartNumber, StockQuantity, Cost
        FROM products;";

        public const string GetSalesCustsByDaySql = @"
        
        SELECT 
            p.Name AS ProductName,
            p.PartNumber,
            SUM(sm.Quantity * p.Cost) / SUM(sm.Quantity) AS AverageCostPerUnit,
            SUM(sm.Quantity * p.Cost) AS TotalCost,
            SUM(sm.Quantity) AS TotalQuantity
            FROM 
                StockMovements sm
            INNER JOIN 
                Products p ON sm.ProductId = p.Id
            WHERE 
                sm.MovementType = 1
                AND DATE(sm.MovementDate) = @MovementDate
            GROUP BY 
                p.Name, p.PartNumber;";
    }
}
