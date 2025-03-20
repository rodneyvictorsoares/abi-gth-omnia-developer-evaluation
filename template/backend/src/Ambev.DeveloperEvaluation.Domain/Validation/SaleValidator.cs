using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator()
        {
            RuleFor(sale => sale.SaleNumber)
                .NotEmpty().WithMessage("Sale number is required!")
                .MaximumLength(50).WithMessage("Sale number must not exceed 50 characters.");

            RuleFor(sale => sale.SaleDate)
                .NotEmpty().WithMessage("Sale date is required!")
                .Must(BeAvalidDate).WithMessage("Sale date must be a valid date in format yyyy-MM-dd");

            RuleFor(sale => sale.Customer)
                .NotEmpty().WithMessage("Customer is required!")
                .MaximumLength(100).WithMessage("Customer must not exceed 100 characters.");

            RuleFor(sale => sale.Branch)
                .NotEmpty().WithMessage("Branch is required!")
                .MaximumLength(100).WithMessage("Branch must not exceed 100 characters.");

            RuleFor(sale => sale.TotalAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Total amount must be non-negative");

            RuleFor(sale => sale.Items)
                .NotEmpty().WithMessage("At least one sale item is required.");
        }

        private bool BeAvalidDate(DateTime date)
        {
            return date != default(DateTime);
        }
    }
}
