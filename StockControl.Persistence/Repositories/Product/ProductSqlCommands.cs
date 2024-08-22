namespace StockControl.Persistence.Repositories.Producto
{
    public static class ProductSqlCommands
    {
        public const string CreateProduct = @"
        INSERT INTO Products (Name, PartNumber, StockQuantity, Cost)
        VALUES (@Name, @PartNumber, @StockQuantity, @Cost);
        SELECT LAST_INSERT_ID();";

        public const string InsertMovementSql = @"
        INSERT INTO StockMovements (ProductId, Quantity, MovementType, MovementDate)
        VALUES (@ProductId, @Quantity, @MovementType, NOW());";

        public const string GetProductById = @"
        SELECT Id, Name, PartNumber, StockQuantity, Cost 
        FROM Products 
        WHERE Id = @Id;";

        public const string UpdateProduct = @"
        UPDATE Products
        SET StockQuantity = @StockQuantity
        WHERE Id = @ProductId;";

        public const string DeleteProduct = @"
        DELETE FROM Products 
        WHERE Id = @Id;";

        public const string GetAllProducts = @"
        SELECT Id, Name, PartNumber, StockQuantity, Cost
        FROM products;";

        public const string GetSalesCustsByDay = @"
        
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
                sm.MovementType = 0
                AND DATE(sm.MovementDate) = @MovementDate
            GROUP BY 
                p.Name, p.PartNumber;";
    }
}
