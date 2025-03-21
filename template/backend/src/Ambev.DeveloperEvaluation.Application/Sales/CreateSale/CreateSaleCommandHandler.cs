using Ambev.DeveloperEvaluation.Common.Messaging;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using MediatR;
using Serilog;


namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Handles the creation of a new sale record.
    /// </summary>
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly DefaultContext _context;
        private readonly IEventPublisher _eventPublisher;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSaleCommandHandler"/> class.
        /// </summary>
        /// <param name="context">The EF Core context for accessing sales data.</param>
        /// <param name="eventPublisher">The event publisher to send events to the message broker.</param>
        public CreateSaleCommandHandler(DefaultContext context, IEventPublisher eventPublisher)
        {
            _context = context;
            _eventPublisher = eventPublisher;
        }

        /// <summary>
        /// Handles the creation of a new sale, applying business rules, validations, and publishing a SaleCreated event.
        /// </summary>
        /// <param name="request">The command containing sale data.</param>
        /// <param name="cancellationToken">A token for cancelling the operation.</param>
        /// <returns>A <see cref="CreateSaleResult"/> with the sale identifier and a message.</returns>
        public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = new Sale(request.SaleNumber, request.SaleDate, request.Customer, request.Branch);

            foreach (var itemDto in request.Items)
            {
                decimal discount = 0m;
                if (itemDto.Quantity >= 10 && itemDto.Quantity <= 20)
                    discount = itemDto.Quantity * itemDto.UnitPrice * 0.20m;
                else if (itemDto.Quantity >= 4 && itemDto.Quantity < 10)
                    discount = itemDto.Quantity * itemDto.UnitPrice * 0.10m;

                decimal totalItemAmount = (itemDto.Quantity * itemDto.UnitPrice) - discount;

                var saleItem = new SaleItem(sale.Id, itemDto.Product, itemDto.Quantity, itemDto.UnitPrice, discount, totalItemAmount);
                sale.AddItem(saleItem);
            }

            var validationResult = sale.Validate();
            if (!validationResult.IsValid)
            {
                throw new Exception("Sale validation failed: " + string.Join(", ", validationResult.Errors.Select(e => e.Detail)));
            }

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync(cancellationToken);

            Log.Information("SaleCreated: Sale {SaleId} created successfully.", sale.Id);

            await _eventPublisher.PublishEventAsync("SaleCreated", new { SaleId = sale.Id }, cancellationToken);

            return new CreateSaleResult
            {
                SaleId = sale.Id,
                Message = "Sale created successfully."
            };
        }
    }
}
