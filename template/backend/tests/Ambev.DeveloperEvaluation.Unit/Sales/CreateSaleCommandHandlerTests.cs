using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Common.Messaging;
using Ambev.DeveloperEvaluation.ORM;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Sales
{
    /// <summary>
    /// Unit tests for the CreateSaleCommandHandler to validate business rules.
    /// </summary>
    public class CreateSaleCommandHandlerTests
    {
        /// <summary>
        /// Creates an in-memory instance of DefaultContext for testing.
        /// </summary>
        /// <returns>An instance of DefaultContext.</returns>
        private DefaultContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid())
                .Options;
            return new DefaultContext(options);
        }

        /// <summary>
        /// Tests that when quantity is below 4, no discount is applied.
        /// </summary>
        [Fact]
        public async Task Handle_ShouldApplyNoDiscount_WhenQuantityLessThan4()
        {
            // Arrange
            var context = GetInMemoryContext();
            var eventPublisherMock = new Mock<IEventPublisher>();
            var handler = new CreateSaleCommandHandler(context, eventPublisherMock.Object);

            var command = new CreateSaleCommand
            {
                SaleNumber = "SALE001",
                SaleDate = DateTime.UtcNow,
                Customer = "Customer A",
                Branch = "Branch A",
                Items = new List<CreateSaleItemDto>
                {
                    new CreateSaleItemDto
                    {
                        Product = "Product A",
                        Quantity = 3, // below 4, so no discount
                        UnitPrice = 10
                    }
                }
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            var sale = context.Sales.Include(s => s.Items).FirstOrDefault(s => s.Id == result.SaleId);
            sale.Should().NotBeNull();
            sale.Items.Should().HaveCount(1);
            var item = sale.Items.First();
            item.Discount.Should().Be(0);
            item.TotalItemAmount.Should().Be(3 * 10);
        }

        /// <summary>
        /// Tests that when quantity is between 4 and 9, a 10% discount is applied.
        /// </summary>
        [Fact]
        public async Task Handle_ShouldApplyTenPercentDiscount_WhenQuantityBetween4And9()
        {
            // Arrange
            var context = GetInMemoryContext();
            var eventPublisherMock = new Mock<IEventPublisher>();
            var handler = new CreateSaleCommandHandler(context, eventPublisherMock.Object);

            var command = new CreateSaleCommand
            {
                SaleNumber = "SALE002",
                SaleDate = DateTime.UtcNow,
                Customer = "Customer B",
                Branch = "Branch B",
                Items = new List<CreateSaleItemDto>
                {
                    new CreateSaleItemDto
                    {
                        Product = "Product B",
                        Quantity = 5, // between 4 and 9: 10% discount
                        UnitPrice = 20
                    }
                }
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            var sale = context.Sales.Include(s => s.Items).FirstOrDefault(s => s.Id == result.SaleId);
            sale.Should().NotBeNull();
            sale.Items.Should().HaveCount(1);
            var item = sale.Items.First();
            decimal expectedDiscount = 5 * 20 * 0.10m;
            item.Discount.Should().Be(expectedDiscount);
            item.TotalItemAmount.Should().Be((5 * 20) - expectedDiscount);
        }

        /// <summary>
        /// Tests that when quantity is between 10 and 20, a 20% discount is applied.
        /// </summary>
        [Fact]
        public async Task Handle_ShouldApplyTwentyPercentDiscount_WhenQuantityBetween10And20()
        {
            // Arrange
            var context = GetInMemoryContext();
            var eventPublisherMock = new Mock<IEventPublisher>();
            var handler = new CreateSaleCommandHandler(context, eventPublisherMock.Object);

            var command = new CreateSaleCommand
            {
                SaleNumber = "SALE003",
                SaleDate = DateTime.UtcNow,
                Customer = "Customer C",
                Branch = "Branch C",
                Items = new List<CreateSaleItemDto>
                {
                    new CreateSaleItemDto
                    {
                        Product = "Product C",
                        Quantity = 15, // between 10 and 20: 20% discount
                        UnitPrice = 30
                    }
                }
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            var sale = context.Sales.Include(s => s.Items).FirstOrDefault(s => s.Id == result.SaleId);
            sale.Should().NotBeNull();
            sale.Items.Should().HaveCount(1);
            var item = sale.Items.First();
            decimal expectedDiscount = 15 * 30 * 0.20m;
            item.Discount.Should().Be(expectedDiscount);
            item.TotalItemAmount.Should().Be((15 * 30) - expectedDiscount);
        }

        /// <summary>
        /// Tests that when quantity exceeds 20, the sale validation fails.
        /// </summary>
        [Fact]
        public async Task Handle_ShouldThrowException_WhenQuantityExceedsTwenty()
        {
            // Arrange
            var context = GetInMemoryContext();
            var eventPublisherMock = new Mock<IEventPublisher>();
            var handler = new CreateSaleCommandHandler(context, eventPublisherMock.Object);

            var command = new CreateSaleCommand
            {
                SaleNumber = "SALE004",
                SaleDate = DateTime.UtcNow,
                Customer = "Customer D",
                Branch = "Branch D",
                Items = new List<CreateSaleItemDto>
                {
                    new CreateSaleItemDto
                    {
                        Product = "Product D",
                        Quantity = 25, // exceeds allowed maximum
                        UnitPrice = 40
                    }
                }
            };

            // Act & Assert: Expect an exception due to failed validation
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
