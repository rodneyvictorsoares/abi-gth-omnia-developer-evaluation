namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale
{
    /// <summary>
    /// Represents the response after deleting a sale.
    /// </summary>
    public class DeleteSaleResponse
    {
        /// <summary>
        /// Gets or sets the sale identifier.
        /// </summary>
        public Guid SaleId { get; set; }

        /// <summary>
        /// Gets or sets a message indicating the outcome of the deletion.
        /// </summary>
        public string Message { get; set; }
    }
}
