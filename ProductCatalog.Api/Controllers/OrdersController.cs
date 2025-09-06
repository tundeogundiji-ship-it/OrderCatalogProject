using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Dtos.Authentication;
using ProductCatalog.Application.Dtos.Orders;
using ProductCatalog.Application.Features.Accounts.Requests.Commands;
using ProductCatalog.Application.Features.Accounts.Requests.Queries;
using ProductCatalog.Application.Features.Orders.Requests.Commands;
using ProductCatalog.Application.Features.Orders.Requests.Queries;
using ProductCatalog.Application.Responses;

namespace ProductCatalog.Api.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/Orders")]
    [Authorize]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IMediator _mediator;
        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [MapToApiVersion(1)]
        [HttpPost()]
        public async Task<ActionResult<CustomResult<Guid>>> CreateOrder([FromBody] CreateOrderDto request)
        {

            var command = new CreateOrderCommand() { CreateOrderDto = request };
            var response = await _mediator.Send(command);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [MapToApiVersion(1)]
        [HttpGet]
        public async Task<ActionResult<CustomResult<IEnumerable<GetOrderDto>>>> Get()
        {
            var orders = await _mediator.Send(new GetOrdersRequest());
            return Ok(orders);
        }
    }
}
