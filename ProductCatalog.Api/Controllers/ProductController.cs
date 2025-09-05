using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Dtos.Authentication;
using ProductCatalog.Application.Dtos.Products;
using ProductCatalog.Application.Features.Accounts.Requests.Commands;
using ProductCatalog.Application.Features.Products.Requests.Commands;
using ProductCatalog.Application.Features.Products.Requests.Queries;
using ProductCatalog.Application.Models;
using ProductCatalog.Application.Responses;

namespace ProductCatalog.Api.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/Products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [MapToApiVersion(1)]
        [HttpGet]
        public async Task<ActionResult<CustomResult<IEnumerable<GetProductDto>>>> Get()
        {
            var products = await _mediator.Send(new GetProductRequestList());
            return Ok(products);
        }

        [MapToApiVersion(1)]
        [HttpGet("{productId}", Name = "GetProduct")]
        public async Task<ActionResult<CustomResult<GetProductDto>>> Get(Guid productId)
        {
            var product = await _mediator.Send(new GetSingleProductRequest()
            {
                ProductId = productId
            });
            return Ok(product);
        }

        [MapToApiVersion(1)]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CustomResult<ProductResponse>>> CreateProduct([FromBody] CreateProductDto request)
        {
            var command = new CreateProductCommand() { CreateProductDto = request };
            var response = await _mediator.Send(command);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return CreatedAtRoute("GetProduct",
                new
                {
                    productId = response.Value.productId
                });
        }

        [MapToApiVersion(1)]
        [HttpPut("{productId}")]
        [Authorize]
        public async Task<ActionResult<CustomResult<ProductResponse>>> UpdateCategory(Guid productId, UpdateProductDto request)
        {
            request.Id = productId;
            var command = new UpdateProductCommand() { UpdateProductDto = request };
            var response = await _mediator.Send(command);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return NoContent();
        }

        [MapToApiVersion(1)]
        [HttpDelete("{productId}")]
        [Authorize]
        public async Task<ActionResult<CustomResult<ProductResponse>>> DeleteCategory(Guid productId)
        {
            var command = new DeleteProductCommand() { ProductId = productId };
            var response = await _mediator.Send(command);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return NoContent();
        }

    }
}
