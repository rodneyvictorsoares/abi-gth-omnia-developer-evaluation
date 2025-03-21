using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Represents a query to retrieve a sale record by its identifier.
    /// </summary>
    public class GetSaleQuery : IRequest<GetSaleResult>
    {
        /// <summary>
        /// Gets or sets the identifier of the sale to be retrieved.
        /// </summary>
        public Guid SaleId { get; set; }
    }

    /// <summary>
    /// Represents the data transfer object for a sale record.
    /// </summary>
    public class GetSaleResult
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
        /// Gets or sets the branch where the sale occurred.
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
        public List<GetSaleItemDto> Items { get; set; }
    }

    /// <summary>
    /// Represents the data transfer object for a sale item within a sale query result.
    /// </summary>
    public class GetSaleItemDto
    {
        /// <summary>
        /// Gets or sets the sale item identifier.
        /// </summary>
        public Guid SaleItemId { get; set; }

        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product.
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
        /// Gets or sets the total amount for the sale item.
        /// </summary>
        public decimal TotalItemAmount { get; set; }
    }
}
