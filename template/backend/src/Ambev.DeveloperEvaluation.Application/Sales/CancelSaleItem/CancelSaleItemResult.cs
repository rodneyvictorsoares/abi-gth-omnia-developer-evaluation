namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem
{
    /// <summary>
    /// Represents the result of canceling a sale item.
    /// </summary>
    public class CancelSaleItemResult
    {
        /// <summary>
        /// Gets or sets the identifier of the cancelled sale item.
        /// </summary>
        public Guid SaleItemId { get; set; }

        /// <summary>
        /// Gets or sets a message indicating the outcome of the operation.
        /// </summary>
        public string Message { get; set; }
    }
}
