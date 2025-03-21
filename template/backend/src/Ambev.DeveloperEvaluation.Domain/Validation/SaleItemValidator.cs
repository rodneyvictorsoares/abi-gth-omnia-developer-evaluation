using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class SaleItemValidator : AbstractValidator<SaleItem>
    {
        public SaleItemValidator()
        {
            RuleFor(item => item.Product)
                .NotEmpty().WithMessage("Product is required!")
                .MaximumLength(100).WithMessage("Product must not exceed 100 charcters.");

            RuleFor(item => item.Quantity)
                .InclusiveBetween(1, 20).WithMessage("Quantity must be between 1 and 20.");

            RuleFor(item => item.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Unit price must be non-negative.");

            RuleFor(item => item.Discount)
                .GreaterThanOrEqualTo(0).WithMessage("Discount must be non-negative.");

            RuleFor(item => item.TotalItemAmount)
                .Equal(item => (item.Quantity * item.UnitPrice) - item.Discount)
                .WithMessage("Total item amount must equal (Quantity * UnitPrice) minus Discount.");

        }
    }
}
