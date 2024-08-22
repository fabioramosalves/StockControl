using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockControl.Application.DataBase.ProductStock.Commands;
using StockControl.Application.DataBase.Product.Queries;
using StockControl.Application.DataBase.ProductStock.Queries;

namespace StockControl.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result }, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery { Id = id });
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand { Id = id });
            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("stock")]
        public async Task<IActionResult> UpdateStock([FromBody] UpdateStockCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok();
            }

            return BadRequest(new { result.Message });
        }

        [HttpGet("average-cost")]
        public async Task<IActionResult> GetAverageCost([FromQuery] DateTime movementDate)
        {
            var command = new GetSalesCustsByDayQuery
            {
                MovementDate = movementDate
            };

            var result = await _mediator.Send(command);

            if (result == null || !result.Any())
            {
                return NotFound(new { message = "No outbound movements found on the specified date." });
            }

            return Ok(result);
        }
    }
}
