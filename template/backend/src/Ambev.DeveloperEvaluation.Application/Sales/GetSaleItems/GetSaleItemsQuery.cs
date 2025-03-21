using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleItems
{
    /// <summary>
    /// Represents a query to retrieve the items of a specific sale.
    /// </summary>
    public class GetSaleItemsQuery : IRequest<GetSaleItemsResult>
    {
        /// <summary>
        /// Gets or sets the identifier of the sale.
        /// </summary>
        public Guid SaleId { get; set; }
    }

    /// <summary>
    /// Represents the result of a sale items query.
    /// </summary>
    public class GetSaleItemsResult
    {
        /// <summary>
        /// Gets or sets the list of sale item DTOs.
        /// </summary>
        public List<GetSaleItemDto> Items { get; set; }
    }

    /// <summary>
    /// Represents a data transfer object for a sale item.
    /// </summary>
    public class GetSaleItemDto
    {
        /// <summary>
        /// Gets or sets the sale item identifier.
        /// </summary>
        public Guid SaleItemId { get; set; }

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
