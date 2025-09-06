using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Dtos.Authentication;
using ProductCatalog.Application.Dtos.Products;
using ProductCatalog.Application.Features.Accounts.Requests.Queries;
using ProductCatalog.Application.Features.Products.Requests.Queries;
using ProductCatalog.Application.Responses;

namespace ProductCatalog.Api.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/Users")]
    [ApiController]
    [Authorize]

    public class UsersController : ControllerBase
    {
        private IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[MapToApiVersion(1)]
        //[HttpGet]
        //public async Task<ActionResult<CustomResult<IEnumerable<GetUsersDto>>>> Get()
        //{
        //    var users = await _mediator.Send(new GetUserRequestList());
        //    return Ok(users);
        //}

        [MapToApiVersion(1)]
        [HttpGet("")]
        public async Task<ActionResult<CustomResult<GetProductDto>>> Get()
        {
            var user = await _mediator.Send(new GetUserDetailRequest());
            return Ok(user);
        }

    }
}
