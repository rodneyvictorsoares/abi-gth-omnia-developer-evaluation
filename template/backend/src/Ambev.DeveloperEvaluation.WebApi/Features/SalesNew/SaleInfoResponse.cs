namespace Ambev.DeveloperEvaluation.WebApi.Features.SalesNew
{
    /// <summary>
    /// Represents the detailed information of a sale.
    /// </summary>
    public class SaleInfoResponse
    {
        /// <summary>
        /// Gets or sets the sale identifier.
        /// </summary>
        public Guid SaleId { get; set; }

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
        /// Gets or sets the branch where the sale was made.
        /// </summary>
        public string Branch { get; set; }

        /// <summary>
        /// Gets or sets the total sale amount.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whether the sale is cancelled.
        /// </summary>
        public bool Cancelled { get; set; }

        /// <summary>
        /// Gets or sets the list of sale items.
        /// </summary>
        public List<SaleItemInfoResponse> Items { get; set; }
    }

    /// <summary>
    /// Represents the detailed information of a sale item.
    /// </summary>
    public class SaleItemInfoResponse
    {
        ///// <summary>
        ///// Gets or sets the sale item identifier.
        ///// </summary>
        //public Guid SaleItemId { get; set; }

        /// <summary>
        /// Gets or sets the product description.
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the discount applied to the product.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the total amount for this sale item.
        /// </summary>
        public decimal TotalItemAmount { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if the item is cancelled.
        /// </summary>
        public bool Cancelled { get; set; }
    }
}
