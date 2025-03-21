namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItem
{
    /// <summary>
    /// Represents the response after cancelling a sale item.
    /// </summary>
    public class CancelSaleItemResponse
    {
        /// <summary>
        /// Gets or sets the sale item identifier.
        /// </summary>
        public Guid SaleItemId { get; set; }

        /// <summary>
        /// Gets or sets a message indicating the outcome of the cancellation.
        /// </summary>
        public string Message { get; set; }
    }
}
