using Ambev.DeveloperEvaluation.Common.Messaging;
using Ambev.DeveloperEvaluation.ORM;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    /// <summary>
    /// Handles the cancel sale command.
    /// </summary>
    public class CancelSaleCommandHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
    {
        private readonly DefaultContext _context;
        private readonly IEventPublisher _eventPublisher;

        /// <summary>
        /// Initializes a new instance of the <see cref="CancelSaleCommandHandler"/> class.
        /// </summary>
        /// <param name="context">The EF Core context for accessing sales data.</param>
        /// <param name="eventPublisher">The event publisher to send events to the message broker.</param>
        public CancelSaleCommandHandler(DefaultContext context, IEventPublisher eventPublisher)
        {
            _context = context;
            _eventPublisher = eventPublisher;
        }

        /// <summary>
        /// Handles the cancellation of a sale by marking it as cancelled.
        /// </summary>
        /// <param name="request">The cancel sale command containing the sale identifier.</param>
        /// <param name="cancellationToken">A token for cancelling the operation.</param>
        /// <returns>A <see cref="CancelSaleResult"/> with the cancelled sale identifier and a success message.</returns>
        public async Task<CancelSaleResult> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _context.Sales.FirstOrDefaultAsync(s => s.Id == request.SaleId, cancellationToken);

            if (sale == null)
            {
                throw new Exception("Sale not found.");
            }

            // Call the domain method to cancel the sale
            sale.Cancel();
            await _context.SaveChangesAsync(cancellationToken);

            Log.Information("SaleCancelled: Sale {SaleId} cancelled successfully.", sale.Id);

            await _eventPublisher.PublishEventAsync("SaleCancelled", new { SaleId = sale.Id }, cancellationToken);

            return new CancelSaleResult
            {
                SaleId = sale.Id,
                Message = "Sale cancelled successfully."
            };
        }
    }
}
