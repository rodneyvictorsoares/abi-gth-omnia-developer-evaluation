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
                .GreaterThanOrEqualTo(1).WithMessage("Quantity must be 1 or more.");

            RuleFor(item => item.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Unit price must be non-negative.");

            RuleFor(item => item.Discount)
                .GreaterThanOrEqualTo(0).WithMessage("Discount must be non-negative.");

            RuleFor(item => item.TotalItemAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Total item amount must be non-negative.");

        }
    }
}
