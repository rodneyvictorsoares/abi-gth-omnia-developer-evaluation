namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    /// <summary>
    /// Represents the result of a delete sale operation.
    /// </summary>
    public class DeleteSaleResult
    {
        /// <summary>
        /// Gets or sets the identifier of the deleted sale.
        /// </summary>
        public Guid SaleId { get; set; }

        /// <summary>
        /// Gets or sets a message indicating the outcome of the operation.
        /// </summary>
        public string Message { get; set; }
    }
}
