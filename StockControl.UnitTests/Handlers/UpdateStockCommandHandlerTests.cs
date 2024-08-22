using FluentAssertions;
using Moq;
using StockControl.Application.DataBase.ProductStock.Commands;
using StockControl.Application.DataBase.ProductStock.Handlers;
using StockControl.Domain.Entities.Product;
using StockControl.Domain.Entities.Stock;
using StockControl.Domain.Enums;
using StockControl.Domain.Repositories;

namespace StockControl.UnitTests.Handlers
{
    public class UpdateStockCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _produtoRepositoryMock;
        private readonly UpdateStockCommandHandler _handler;

        public UpdateStockCommandHandlerTests()
        {
            _produtoRepositoryMock = new Mock<IProductRepository>();
            _handler = new UpdateStockCommandHandler(_produtoRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnError_WhenProductNotFound()
        {
            var command = new UpdateStockCommand
            {
                ProductId = 1,
                Quantity = 10,
                MovementType = MovementType.Outbound
            };

            _produtoRepositoryMock.Setup(repo => repo.GetByIdAsync(command.ProductId))
                .ReturnsAsync((ProductEntity)null);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Success.Should().BeFalse();
            result.Message.Should().Be("Product was not found in stock.");

            _produtoRepositoryMock.Verify(repo => repo.GetByIdAsync(command.ProductId), Times.Once);
            _produtoRepositoryMock.Verify(repo => repo.UpdateStockAsync(It.IsAny<StockMovementEntity>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnError_WhenStockIsInsufficient()
        {
            var command = new UpdateStockCommand
            {
                ProductId = 1,
                Quantity = 10,
                MovementType = MovementType.Outbound
            };

            var productEntity = new ProductEntity
            {
                Id = command.ProductId,
                StockQuantity = 5
            };

            _produtoRepositoryMock.Setup(repo => repo.GetByIdAsync(command.ProductId))
                .ReturnsAsync(productEntity);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Success.Should().BeFalse();
            result.Message.Should().Be("Insufficient stock to complete the operation.");

            _produtoRepositoryMock.Verify(repo => repo.GetByIdAsync(command.ProductId), Times.Once);
            _produtoRepositoryMock.Verify(repo => repo.UpdateStockAsync(It.IsAny<StockMovementEntity>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldUpdateStock_WhenMovementIsInbound()
        {
            var command = new UpdateStockCommand
            {
                ProductId = 1,
                Quantity = 10,
                MovementType = MovementType.Inbound
            };

            var productEntity = new ProductEntity
            {
                Id = command.ProductId,
                StockQuantity = 5
            };

            _produtoRepositoryMock.Setup(repo => repo.GetByIdAsync(command.ProductId))
                .ReturnsAsync(productEntity);

            _produtoRepositoryMock.Setup(repo => repo.UpdateStockAsync(It.IsAny<StockMovementEntity>()))
                .ReturnsAsync(true);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Success.Should().BeTrue();
            result.Message.Should().NotBeNull();

            _produtoRepositoryMock.Verify(repo => repo.GetByIdAsync(command.ProductId), Times.Once);
            _produtoRepositoryMock.Verify(repo => repo.UpdateStockAsync(It.Is<StockMovementEntity>(
                s => s.StockQuantity == 15 && s.ProductId == command.ProductId)), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldUpdateStock_WhenMovementIsOutbound()
        {
            var command = new UpdateStockCommand
            {
                ProductId = 1,
                Quantity = 2,
                MovementType = MovementType.Outbound
            };

            var productEntity = new ProductEntity
            {
                Id = command.ProductId,
                StockQuantity = 10
            };

            _produtoRepositoryMock.Setup(repo => repo.GetByIdAsync(command.ProductId))
                .ReturnsAsync(productEntity);

            _produtoRepositoryMock.Setup(repo => repo.UpdateStockAsync(It.IsAny<StockMovementEntity>()))
                .ReturnsAsync(true);

            var result = await _handler.Handle(command, CancellationToken.None);


            result.Success.Should().BeTrue();
            result.Message.Should().NotBeNull();

            _produtoRepositoryMock.Verify(repo => repo.GetByIdAsync(command.ProductId), Times.Once);
            _produtoRepositoryMock.Verify(repo => repo.UpdateStockAsync(It.Is<StockMovementEntity>(
                s => s.StockQuantity == 8 && s.ProductId == command.ProductId)), Times.Once);
        }
    }
}
