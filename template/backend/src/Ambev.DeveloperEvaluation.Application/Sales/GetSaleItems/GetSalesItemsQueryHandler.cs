using Ambev.DeveloperEvaluation.ORM;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleItems
{
    /// <summary>
    /// Handles the query to retrieve the items of a sale.
    /// </summary>
    public class GetSalesItemsQueryHandler : IRequestHandler<GetSaleItemsQuery, GetSaleItemsResult>
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSaleItemsQueryHandler"/> class.
        /// </summary>
        /// <param name="context">The EF Core context for accessing sale items data.</param>
        public GetSalesItemsQueryHandler(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Handles the query to retrieve sale items by sale identifier.
        /// </summary>
        /// <param name="request">The query containing the sale identifier.</param>
        /// <param name="cancellationToken">A token for cancelling the operation.</param>
        /// <returns>A <see cref="GetSaleItemsResult"/> containing the list of sale item DTOs.</returns>
        public async Task<GetSaleItemsResult> Handle(GetSaleItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _context.SaleItems
                .Where(si => si.SaleId == request.SaleId)
                .Select(si => new GetSaleItemDto
                {
                    SaleItemId = si.Id,
                    Product = si.Product,
                    Quantity = si.Quantity,
                    UnitPrice = si.UnitPrice,
                    Discount = si.Discount,
                    TotalItemAmount = si.TotalItemAmount,
                    Cancelled = si.Cancelled
                })
                .ToListAsync(cancellationToken);

            return new GetSaleItemsResult { Items = items };
        }
    }
}
