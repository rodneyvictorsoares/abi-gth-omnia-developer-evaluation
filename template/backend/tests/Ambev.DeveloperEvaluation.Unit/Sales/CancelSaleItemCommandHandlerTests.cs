using Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;
using Ambev.DeveloperEvaluation.Common.Messaging;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Sales
{
    /// <summary>
    /// Unit tests for CancelSaleItemCommandHandler to verify the cancellation of a specific sale item.
    /// </summary>
    public class CancelSaleItemCommandHandlerTests
    {
        private DefaultContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(databaseName: "CancelSaleItemTestDb_" + Guid.NewGuid())
                .Options;
            return new DefaultContext(options);
        }

        [Fact]
        public async Task Handle_ShouldCancelSaleItem_WhenItemExists()
        {
            // Arrange: Cria uma venda e adiciona um item
            var context = GetInMemoryContext();
            var eventPublisherMock = new Mock<IEventPublisher>();
            var sale = new Sale("SALE_ITEM", DateTime.UtcNow.AddDays(-1), "Customer Item", "Branch Item");
            var saleItem = new SaleItem(sale.Id, "Product X", 5, 10, 5, (5 * 10) - 5);
            sale.AddItem(saleItem);
            context.Sales.Add(sale);
            await context.SaveChangesAsync();

            var command = new CancelSaleItemCommand
            {
                SaleId = sale.Id,
                SaleItemId = saleItem.Id
            };
            var handler = new CancelSaleItemCommandHandler(context, eventPublisherMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert: Verifica se o item foi cancelado
            var updatedItem = await context.SaleItems.FirstOrDefaultAsync(i => i.Id == saleItem.Id);
            updatedItem.Should().NotBeNull();
            updatedItem.Cancelled.Should().BeTrue();
            result.Message.Should().Be("Sale item cancelled successfully.");
        }
    }
}
