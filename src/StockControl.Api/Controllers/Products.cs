using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockControl.Application.DataBase.ProductStock.Commands;
using StockControl.Application.DataBase.Product.Queries;

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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(command);
            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
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

        [HttpPost("add-stock")]
        public async Task<IActionResult> AddStock([FromBody] AddStockCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok(new { message = "Stock updated successfully." });
            }

            return BadRequest(new { message = "Failed to update stock." });
        }
    }
}
