using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    /// <summary>
    /// Validator for updating a sale. Only validates header properties (SaleNumber, SaleDate, Customer and Branch).
    /// </summary>
    public class UpdateSaleValidator : AbstractValidator<Sale>
    {
        public UpdateSaleValidator()
        {
            RuleFor(sale => sale.SaleNumber)
                .NotEmpty().WithMessage("Sale number is required.")
                .MaximumLength(50).WithMessage("Sale number must not exceed 50 characters.");

            RuleFor(sale => sale.SaleDate)
                .NotEmpty().WithMessage("Sale date is required.");

            RuleFor(sale => sale.Customer)
                .NotEmpty().WithMessage("Customer is required.")
                .MaximumLength(100).WithMessage("Customer must not exceed 100 characters.");

            RuleFor(sale => sale.Branch)
                .NotEmpty().WithMessage("Branch is required.")
                .MaximumLength(100).WithMessage("Branch must not exceed 100 characters.");

            RuleFor(sale => sale.TotalAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Total amount must be non-negative.");

        }
    }
}
