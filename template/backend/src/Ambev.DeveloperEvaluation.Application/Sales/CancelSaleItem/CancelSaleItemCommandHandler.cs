using Ambev.DeveloperEvaluation.Common.Messaging;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem
{
    /// <summary>
    /// Handles the cancel sale item command.
    /// </summary>
    public class CancelSaleItemCommandHandler : IRequestHandler<CancelSaleItemCommand, CancelSaleItemResult>
    {
        private readonly DefaultContext _context;
        private readonly IEventPublisher _eventPublisher;

        /// <summary>
        /// Initializes a new instance of the <see cref="CancelSaleItemCommandHandler"/> class.
        /// </summary>
        /// <param name="context">The EF Core context for accessing sales data.</param>
        /// <param name="eventPublisher">The event publisher to send events to the message broker.</param>
        public CancelSaleItemCommandHandler(DefaultContext context, IEventPublisher eventPublisher)
        {
            _context = context;
            _eventPublisher = eventPublisher;
        }

        /// <summary>
        /// Handles the cancellation of a specific sale item.
        /// </summary>
        /// <param name="request">The command containing the sale and sale item identifiers.</param>
        /// <param name="cancellationToken">A token for cancelling the operation.</param>
        /// <returns>A <see cref="CancelSaleItemResult"/> with the cancelled sale item identifier and a success message.</returns>
        public async Task<CancelSaleItemResult> Handle(CancelSaleItemCommand request, CancellationToken cancellationToken)
        {
            var saleItem = await _context.SaleItems
                .FirstOrDefaultAsync(si => si.Id == request.SaleItemId && si.SaleId == request.SaleId, cancellationToken);

            if (saleItem == null)
                throw new Exception("Sale item not found.");

            saleItem.Cancel();

            await _context.SaveChangesAsync(cancellationToken);

            Log.Information("ItemCancelled: Sale item {SaleItemId} from sale {SaleId} cancelled successfully.", saleItem.Id, saleItem.SaleId);
            
            await _eventPublisher.PublishEventAsync("ItemCancelled", new { SaleItemId = saleItem.Id, SaleId = saleItem.SaleId }, cancellationToken);

            return new CancelSaleItemResult
            {
                SaleItemId = saleItem.Id,
                Message = "Sale item cancelled successfully."
            };
        }
    }
}
