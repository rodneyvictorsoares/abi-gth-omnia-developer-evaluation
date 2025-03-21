using Ambev.DeveloperEvaluation.ORM;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleQueryHandler : IRequestHandler<GetSaleQuery, GetSaleResult>
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSaleQueryHandler"/> class.
        /// </summary>
        /// <param name="context">The EF Core context for accessing sales data.</param>
        public GetSaleQueryHandler(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Handles the query to retrieve a sale record.
        /// </summary>
        /// <param name="request">The query containing the sale identifier.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A <see cref="GetSaleResult"/> containing the sale data.</returns>
        public async Task<GetSaleResult> Handle(GetSaleQuery request, CancellationToken cancellationToken)
        {
            var sale = await _context.Sales
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == request.SaleId, cancellationToken);

            if (sale == null)
            {
                throw new Exception("Sale not found.");
            }

            return new GetSaleResult
            {
                SaleId = sale.Id,
                SaleNumber = sale.SaleNumber,
                SaleDate = sale.SaleDate,
                Customer = sale.Customer,
                Branch = sale.Branch,
                TotalAmount = sale.TotalAmount,
                Cancelled = sale.Cancelled,
                Items = sale.Items.Select(i => new GetSaleItemDto
                {
                    Product = i.Product,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Discount = i.Discount,
                    TotalItemAmount = i.TotalItemAmount
                }).ToList()
            };
        }
    }
}
