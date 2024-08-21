
--*************DDL commands*************

CREATE TABLE Products (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100) NOT NULL,
    PartNumber VARCHAR(50) NOT NULL,
    StockQuantity INT NOT NULL,
    AverageCost DECIMAL(10, 2) NOT NULL
);

CREATE TABLE StockMovements (
    StockMovementId INT PRIMARY KEY AUTO_INCREMENT,
    ProductId INT NOT NULL,
    Quantity INT NOT NULL,
    MovementType ENUM('Inbound', 'Outbound') NOT NULL,
    MovementDate DATETIME NOT NULL,
    TotalCost DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (ProductId) REFERENCES Products(Id)
);


CREATE TABLE Logs (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Message TEXT NULL,
    MessageTemplate TEXT NULL,
	Template TEXT NULL,
    Level VARCHAR(128) NULL,
    TimeStamp DATETIME NULL,
    Exception TEXT NULL,
    Properties TEXT NULL,
    LogEvent JSON NULL
);


--*************DML commands*************

INSERT INTO Products (Name, PartNumber, StockQuantity, AverageCost)
VALUES 
('Vela de Ignição', 'VI-001', 0, 182.90),
('Junta do Cabeçote', 'JC-001', 0, 265.72),
('Amortecedor Dianteiro', 'AD-001', 0, 425.45),
('Disco de Freio', 'DF-001', 0, 129.90)