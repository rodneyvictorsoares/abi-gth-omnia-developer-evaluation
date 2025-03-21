using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem
{
    /// <summary>
    /// Represents a command to cancel a specific sale item.
    /// </summary>
    public class CancelSaleItemCommand : IRequest<CancelSaleItemResult>
    {
        /// <summary>
        /// Gets or sets the identifier of the sale.
        /// </summary>
        public Guid SaleId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the sale item to cancel.
        /// </summary>
        public Guid SaleItemId { get; set; }
    }
}
