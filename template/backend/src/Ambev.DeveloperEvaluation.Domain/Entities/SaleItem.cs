﻿using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents an item within a sale, including product details and pricing information.
    /// This entity includes validation rules similar to those applied in the User entity.
    /// </summary>
    public class SaleItem : BaseEntity
    {
        /// <summary>
        /// Gets or sets the identifier of the sale this item belongs to.
        /// </summary>
        public Guid SaleId { get; set; }

        /// <summary>
        /// Gets or sets the product description or identifier.
        /// Must not be null or empty.
        /// </summary>
        public string Product { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity of the product.
        /// Must be a numeric value between 1 and 20.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product.
        /// Must be a numeric value greater than or equal to zero.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the discount applied to the product.
        /// Must be a numeric value greater than or equal to zero.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the total amount for this sale item (after discount).
        /// Must be a numeric value greater than or equal to zero.
        /// </summary>
        public decimal TotalItemAmount { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whether this sale item has been cancelled.
        /// </summary>
        public bool Cancelled { get; private set; } = false;

        /// <summary>
        /// Navigation property to the related Sale entity.
        /// </summary>
        public Sale Sale { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SaleItem"/> class with the specified parameters.
        /// </summary>
        /// <param name="saleId">The identifier of the sale this item belongs to.</param>
        /// <param name="product">The product name or description.</param>
        /// <param name="quantity">The quantity of the product.</param>
        /// <param name="unitPrice">The unit price of the product.</param>
        /// <param name="discount">The discount applied to the product.</param>
        /// <param name="totalItemAmount">The total amount for this item (after discount).</param>
        public SaleItem(Guid saleId, string product, int quantity, decimal unitPrice, decimal discount, decimal totalItemAmount)
        {
            Id = Guid.NewGuid();
            SaleId = saleId;
            Product = product;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;
            TotalItemAmount = totalItemAmount;
        }

        /// <summary>
        /// Cancels this sale item by marking it as cancelled.
        /// </summary>
        public void Cancel()
        {
            Cancelled = true;
        }

        /// <summary>
        /// Validates the sale item entity using the SaleItemValidator rules.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing:
        /// - IsValid: Indicates whether all validation rules passed.
        /// - Errors: Collection of validation errors if any rule fails.
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new SaleItemValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
