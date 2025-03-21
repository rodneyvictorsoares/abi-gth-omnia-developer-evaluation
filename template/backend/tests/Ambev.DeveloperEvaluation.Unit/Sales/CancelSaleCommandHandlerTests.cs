using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
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
    /// Unit tests for CancelSaleCommandHandler to verify sale cancellation.
    /// </summary>
    public class CancelSaleCommandHandlerTests
    {
        private DefaultContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(databaseName: "CancelSaleTestDb_" + Guid.NewGuid())
                .Options;
            return new DefaultContext(options);
        }

        [Fact]
        public async Task Handle_ShouldCancelSale_WhenSaleExists()
        {
            // Arrange: Cria uma venda previamente cadastrada
            var context = GetInMemoryContext();
            var eventPublisherMock = new Mock<IEventPublisher>();
            var sale = new Sale("SALE_CANCEL", DateTime.UtcNow.AddDays(-1), "Customer Cancel", "Branch Cancel");
            context.Sales.Add(sale);
            await context.SaveChangesAsync();

            var command = new CancelSaleCommand { SaleId = sale.Id };
            var handler = new CancelSaleCommandHandler(context, eventPublisherMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert: Verifica se a venda foi marcada como cancelada
            var cancelledSale = await context.Sales.FirstOrDefaultAsync(s => s.Id == sale.Id);
            cancelledSale.Should().NotBeNull();
            cancelledSale.Cancelled.Should().BeTrue();
            result.Message.Should().Be("Sale cancelled successfully.");
        }
    }
}
