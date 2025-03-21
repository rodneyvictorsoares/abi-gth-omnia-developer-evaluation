using Ambev.DeveloperEvaluation.ORM;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    /// <summary>
    /// Handles the delete sale command.
    /// </summary>
    public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResult>
    {

        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteSaleCommandHandler"/> class.
        /// </summary>
        /// <param name="context">The EF Core context for accessing sales data.</param>
        public DeleteSaleCommandHandler(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Handles the deletion of a sale record.
        /// </summary>
        /// <param name="request">The delete sale command containing the sale identifier.</param>
        /// <param name="cancellationToken">A token for cancelling the operation.</param>
        /// <returns>A <see cref="DeleteSaleResult"/> with the deleted sale identifier and a success message.</returns>
        public async Task<DeleteSaleResult> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _context.Sales.FirstOrDefaultAsync(s => s.Id == request.SaleId, cancellationToken);

            if (sale == null)
            {
                throw new Exception("Sale not found.");
            }

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync(cancellationToken);

            return new DeleteSaleResult
            {
                SaleId = sale.Id,
                Message = "Sale deleted successfully."
            };
        }
    }
}
