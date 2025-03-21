namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Represents the result of executing the CreateSaleCommand.
    /// </summary>
    public class CreateSaleResult
    {
        // <summary>
        /// Gets or sets the identifier of the newly created sale.
        /// </summary>
        public Guid SaleId { get; set; }

        /// <summary>
        /// Gets or sets the message associated with the creation process.
        /// </summary>
        public string Message { get; set; }
    }
}
