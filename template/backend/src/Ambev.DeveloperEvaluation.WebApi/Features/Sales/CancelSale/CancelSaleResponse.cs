namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale
{
    /// <summary>
    /// Represents the response after cancelling a sale.
    /// </summary>
    public class CancelSaleResponse
    {
        /// <summary>
        /// Gets or sets the sale identifier.
        /// </summary>
        public Guid SaleId { get; set; }

        /// <summary>
        /// Gets or sets a message indicating the outcome of the cancellation.
        /// </summary>
        public string Message { get; set; }
    }
}
