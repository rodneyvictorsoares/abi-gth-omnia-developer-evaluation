using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using MediatR;


namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly DefaultContext _context;
        public CreateSaleCommandHandler(DefaultContext context)
        {
            _context = context;
        }

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

            return new CreateSaleResult
            {
                SaleId = sale.Id,
                Message = "Sale created successfully."
            };
        }
    }
}
