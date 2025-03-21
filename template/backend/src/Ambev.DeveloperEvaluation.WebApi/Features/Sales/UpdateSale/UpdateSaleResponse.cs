namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    /// <summary>
    /// Represents the response after updating a sale.
    /// </summary>
    public class UpdateSaleResponse
    {
        /// <summary>
        /// Gets or sets the sale identifier.
        /// </summary>
        public Guid SaleId { get; set; }

        /// <summary>
        /// Gets or sets a message indicating the outcome of the update.
        /// </summary>
        public string Message { get; set; }
    }
}
