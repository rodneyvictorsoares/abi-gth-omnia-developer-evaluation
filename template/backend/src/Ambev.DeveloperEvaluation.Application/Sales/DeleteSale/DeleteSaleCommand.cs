using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    /// <summary>
    /// Represents a command to delete a sale record.
    /// </summary>
    public class DeleteSaleCommand : IRequest<DeleteSaleResult>
    {
        /// <summary>
        /// Gets or sets the identifier of the sale to be deleted.
        /// </summary>
        public Guid SaleId { get; set; }
    }
}
