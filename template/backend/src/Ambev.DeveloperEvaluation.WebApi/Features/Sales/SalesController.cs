using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.Validators;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    /// <summary>
    /// Controller responsible for handling Sales related operations (CRUD).
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="SalesController"/> class.
        /// </summary>
        /// <param name="mediator">The MediatR mediator for handling commands and queries.</param>
        /// <param name="mapper">The AutoMapper instance for mapping between DTOs and command/query objects.</param>
        public SalesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new sale.
        /// </summary>
        /// <param name="request">The sale creation request containing sale details and items.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>An envelope with the created sale identifier and a success message.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            try
            {
                var command = _mapper.Map<CreateSaleCommand>(request);
                var result = await _mediator.Send(command, cancellationToken);

                return Created("GetSale", new ApiResponseWithData<CreateSaleResponse>
                {
                    Success = true,
                    Message = "Sale created successfully",
                    Data = _mapper.Map<CreateSaleResponse>(result)
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse { Success = false, Message = ex.Message });
            }
            
        }

        /// <summary>
        /// Retrieves a sale by its identifier.
        /// </summary>
        /// <param name="id">The sale identifier.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// An envelope with the sale data and a success message.
        /// </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetSaleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSale(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid sale ID.");
            try
            {
                var query = new GetSaleQuery { SaleId = id };
                var result = await _mediator.Send(query, cancellationToken);

                return Ok(_mapper.Map<GetSaleResponse>(result), "Sale retrieved successfully.");
            }
            catch(Exception ex)
            {
                return NotFound(new ApiResponse { Success = false, Message = ex.Message });
            }
            
        }

        /// <summary>
        /// Updates an existing sale.
        /// </summary>
        /// <param name="id">The identifier of the sale to update.</param>
        /// <param name="request">The update sale request containing updated sale details.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>An envelope with the updated sale identifier and a success message.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<UpdateSaleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSale(Guid id, [FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if (id == Guid.Empty)
                return BadRequest("Invalid sale ID.");

            try
            {
                var command = _mapper.Map<UpdateSaleCommand>(request);
                command.SaleId = id;
                var result = await _mediator.Send(command, cancellationToken);

                return Ok(_mapper.Map<UpdateSaleResponse>(result), "Sale updated successfully.");
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse { Success = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Cancels a sale.
        /// </summary>
        /// <param name="id">The identifier of the sale to cancel.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>An envelope with the cancelled sale identifier and a success message.</returns>
        [HttpPatch("{id}/cancel")]
        [ProducesResponseType(typeof(ApiResponseWithData<CancelSaleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CancelSale(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid sale ID.");

            try
            {
                var command = new CancelSaleCommand { SaleId = id };
                var result = await _mediator.Send(command, cancellationToken);

                return Ok(_mapper.Map<CancelSaleResponse>(result), "Sale cancelled successfully.");
            }
            catch(Exception ex)
            {
                return NotFound(new ApiResponse { Success = false, Message = ex.Message });
            }
            
        }

        /// <summary>
        /// Deletes a sale.
        /// </summary>
        /// <param name="id">The identifier of the sale to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>An envelope with the deleted sale identifier and a success message.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<DeleteSaleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSale(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid sale ID.");
            try
            {
                var command = new DeleteSaleCommand { SaleId = id };
                var result = await _mediator.Send(command, cancellationToken);

                return Ok(_mapper.Map<DeleteSaleResponse>(result), "Sale deleted successfully.");
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse { Success = false, Message = ex.Message });
            }
            
        }
    }
}
