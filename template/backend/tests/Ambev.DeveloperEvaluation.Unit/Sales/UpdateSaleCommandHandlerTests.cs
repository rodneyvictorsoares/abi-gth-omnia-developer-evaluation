using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
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
    /// Unit tests for UpdateSaleCommandHandler to validate updating sale header properties.
    /// </summary>
    public class UpdateSaleCommandHandlerTests
    {
        private DefaultContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(databaseName: "UpdateSaleTestDb_" + Guid.NewGuid())
                .Options;
            return new DefaultContext(options);
        }

        [Fact]
        public async Task Handle_ShouldUpdateSaleHeader_WhenDataIsValid()
        {
            // Arrange: Cria uma venda previamente cadastrada
            var context = GetInMemoryContext();
            var eventPublisherMock = new Mock<IEventPublisher>();
            // Cria uma venda de exemplo
            var sale = new Sale("OLD001", DateTime.UtcNow.AddDays(-1), "Old Customer", "Old Branch");
            context.Sales.Add(sale);
            await context.SaveChangesAsync();

            // Prepara o comando de update
            var command = new UpdateSaleCommand
            {
                SaleId = sale.Id,
                SaleNumber = "NEW001",
                SaleDate = DateTime.UtcNow,
                Customer = "New Customer",
                Branch = "New Branch"
            };

            var handler = new UpdateSaleCommandHandler(context, eventPublisherMock.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert: Verifica se os dados foram atualizados corretamente
            var updatedSale = await context.Sales.FirstOrDefaultAsync(s => s.Id == sale.Id);
            updatedSale.Should().NotBeNull();
            updatedSale.SaleNumber.Should().Be("NEW001");
            updatedSale.Customer.Should().Be("New Customer");
            updatedSale.Branch.Should().Be("New Branch");
            result.Message.Should().Be("Sale updated successfully.");
        }
    }
}
