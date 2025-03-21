namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Represents a request to create a new sale.
    /// </summary>
    public class CreateSaleRequest
    {
        /// <summary>
        /// Gets or sets the sale number.
        /// </summary>
        public string SaleNumber { get; set; }

        /// <summary>
        /// Gets or sets the sale date.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Gets or sets the customer name.
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// Gets or sets the branch where the sale occurred.
        /// </summary>
        public string Branch { get; set; }

        /// <summary>
        /// Gets or sets the list of sale items.
        /// </summary>
        public List<CreateSaleItemRequest> Items { get; set; }
    }

    /// <summary>
    /// Represents an item in the create sale request.
    /// </summary>
    public class CreateSaleItemRequest
    {
        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// Gets or sets the quantity for the product.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price for the product.
        /// </summary>
        public decimal UnitPrice { get; set; }
    }
}
