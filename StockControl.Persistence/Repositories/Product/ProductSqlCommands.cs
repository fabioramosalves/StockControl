namespace StockControl.Persistence.Repositories.Producto
{
    public static class ProductSqlCommands
    {
        public const string CreateProduct = @"
        INSERT INTO Products (Name, PartNumber, StockQuantity, AverageCost)
        VALUES (@Name, @PartNumber, @StockQuantity, @AverageCost);
        SELECT LAST_INSERT_ID();";

        public const string AddStockSql = @"
        UPDATE Products
        SET StockQuantity = StockQuantity + @IncomingQuantity
        WHERE Id = @ProductId;";

        public const string InsertMovementSql = @"
        INSERT INTO StockMovements (ProductId, Quantity, MovementType, MovementDate, TotalCost)
        VALUES (@ProductId, @IncomingQuantity, 'Inbound', NOW(), @TotalCost);";

        public const string GetProductById = @"
        SELECT Id, Name, PartNumber, StockQuantity, AverageCost 
        FROM Products 
        WHERE Id = @Id;";

        public const string UpdateProduct = @"
        UPDATE Products
        SET StockQuantity = StockQuantity - @requestedQuantity
        WHERE ProductId = @ProductId;";

        public const string DeleteProduct = @"
        DELETE FROM Products 
        WHERE Id = @Id;";

        public const string GetAllProducts = @"
        SELECT Id, Name, PartNumber, StockQuantity, AverageCost
        FROM products;";
    }
}
