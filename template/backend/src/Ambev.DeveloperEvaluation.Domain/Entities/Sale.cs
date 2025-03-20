﻿using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a sale record in the system, including details about the sale and its items.
    /// This entity follows domain-driven design principles and includes business rules validation.
    /// </summary>
    public class Sale : BaseEntity
    {
        /// <summary>
        /// Gets or sets the sale number.
        /// Must not be null or empty and must follow a defined format.
        /// </summary>
        public string SaleNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date when the sale was made.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Gets or sets the customer for the sale.
        /// Must not be null or empty.
        /// </summary>
        public string Customer { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the branch where the sale was made.
        /// Must not be null or empty.
        /// </summary>
        public string Branch { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total amount of the sale.
        /// Must be a numeric value greater than or equal to zero.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whether the sale is cancelled.
        /// </summary>
        public bool Cancelled { get; set; }

        /// <summary>
        /// Gets the collection of sale items.
        /// There must be at least one item in the sale.
        /// </summary>
        public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();

        /// <summary>
        /// Gets the date and time when the sale was created.
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// Gets the date and time of the last update to the sale.
        /// </summary>
        public DateTime? UpdateAt { get; set; }

        /// <summary>
        /// Initializes a new instance of the Sale class.
        /// </summary>
        public Sale()
        {
            CreateAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Validates the sale entity using the SaleValidator rules.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing:
        /// - IsValid: Indicates whether all validation rules passed.
        /// - Errors: Collection of validation errors if any rule fails.
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new SaleValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }

        /// <summary>
        /// Cancels the sale, marking it as cancelled and updating the timestamp.
        /// </summary>
        public void Cancel()
        {
            Cancelled = true;
            UpdateAt = DateTime.UtcNow;
        }
    }
}
