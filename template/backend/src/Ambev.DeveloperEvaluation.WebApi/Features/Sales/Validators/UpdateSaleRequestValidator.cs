using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.Validators
{
    /// <summary>
    /// Validates the UpdateSaleRequest ensuring that all required fields are valid.
    /// </summary>
    public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
    {
        public UpdateSaleRequestValidator()
        {
            RuleFor(x => x.SaleNumber)
                .NotEmpty().WithMessage("Sale number is required.");

            RuleFor(x => x.SaleDate)
                .NotEmpty().WithMessage("Sale date is required.");

            RuleFor(x => x.Customer)
                .NotEmpty().WithMessage("Customer is required.");

            RuleFor(x => x.Branch)
                .NotEmpty().WithMessage("Branch is required.");
        }
    }
}
