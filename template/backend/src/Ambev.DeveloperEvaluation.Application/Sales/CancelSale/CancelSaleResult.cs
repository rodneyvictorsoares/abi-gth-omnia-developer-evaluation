namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    /// <summary>
    /// Represents the result of a cancel sale operation.
    /// </summary>
    public class CancelSaleResult
    {
        /// <summary>
        /// Gets or sets the identifier of the cancelled sale.
        /// </summary>
        public Guid SaleId { get; set; }

        /// <summary>
        /// Gets or sets a message indicating the outcome of the cancellation.
        /// </summary>
        public string Message { get; set; }
    }
}
