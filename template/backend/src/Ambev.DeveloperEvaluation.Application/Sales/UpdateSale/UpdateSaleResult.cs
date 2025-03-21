namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Represents the result of an update sale operation.
    /// </summary>
    public class UpdateSaleResult
    {
        /// <summary>
        /// Gets or sets the identifier of the updated sale.
        /// </summary>
        public Guid SaleId { get; set; }

        /// <summary>
        /// Gets or sets a message indicating the outcome of the operation.
        /// </summary>
        public string Message { get; set; }
    }
}
