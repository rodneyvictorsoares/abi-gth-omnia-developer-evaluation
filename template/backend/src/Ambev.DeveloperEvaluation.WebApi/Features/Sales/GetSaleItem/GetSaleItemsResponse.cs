using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    /// <summary>
    /// Represents the response containing the list of sale items.
    /// </summary>
    public class GetSaleItemsResponse
    {
        /// <summary>
        /// Gets or sets the list of sale items.
        /// </summary>
        public List<GetSaleItemResponse> Items { get; set; }
    }

    /// <summary>
    /// Represents a data transfer object for a sale item.
    /// </summary>
    public class GetSaleItemResponse
    {
        /// <summary>
        /// Gets or sets the sale item identifier.
        /// </summary>
        public System.Guid SaleItemId { get; set; }

        /// <summary>
        /// Gets or sets the product description.
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the discount applied.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the total amount for the item.
        /// </summary>
        public decimal TotalItemAmount { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if the item is cancelled.
        /// </summary>
        public bool Cancelled { get; set; }
    }
}
