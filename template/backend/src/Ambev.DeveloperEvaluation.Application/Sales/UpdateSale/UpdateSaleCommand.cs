using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Represents a command to update an existing sale record.
    /// </summary>
    public class UpdateSaleCommand : IRequest<UpdateSaleResult>
    {
        /// <summary>
        /// Gets or sets the identifier of the sale to be updated.
        /// </summary>
        public Guid SaleId { get; set; }

        /// <summary>
        /// Gets or sets the updated sale number.
        /// </summary>
        public string SaleNumber { get; set; }

        /// <summary>
        /// Gets or sets the updated sale date.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Gets or sets the updated customer name.
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// Gets or sets the updated branch.
        /// </summary>
        public string Branch { get; set; }
    }
}
