using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.ORM;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Handles the update sale command.
    /// </summary>
    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSaleCommandHandler"/> class.
        /// </summary>
        /// <param name="context">The EF Core context for accessing sales data.</param>
        public UpdateSaleCommandHandler(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Handles the update sale command by updating sale details and saving changes.
        /// </summary>
        /// <param name="request">The update sale command containing the updated sale data.</param>
        /// <param name="cancellationToken">A token for cancelling the operation.</param>
        /// <returns>A <see cref="UpdateSaleResult"/> with the updated sale identifier and a success message.</returns>
        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _context.Sales.FirstOrDefaultAsync(s => s.Id == request.SaleId, cancellationToken);

            if (sale == null)
            {
                throw new Exception("Sale not found.");
            }

            sale.SaleNumber = request.SaleNumber;
            sale.SaleDate = request.SaleDate;
            sale.Customer = request.Customer;
            sale.Branch = request.Branch;
            sale.UpdateAt = DateTime.UtcNow;

            var validator = new UpdateSaleValidator();
            var validationResult = validator.Validate(sale);
            if (!validationResult.IsValid)
            {
                throw new Exception("Sale validation failed: " + string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            await _context.SaveChangesAsync(cancellationToken);

            Log.Information("SaleModified: Sale {SaleId} updated successfully.", sale.Id);

            return new UpdateSaleResult
            {
                SaleId = sale.Id,
                Message = "Sale updated successfully."
            };
        }
    }
}
