using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Dtos.Authentication;
using ProductCatalog.Application.Features.Accounts.Requests.Commands;
using ProductCatalog.Application.Responses;

namespace ProductCatalog.Api.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [MapToApiVersion(1)]
        [HttpPost("Register")]
        public async Task<ActionResult<CustomResult<Guid>>> Create([FromBody] RegisterUserDto request)
        {

            var command = new RegisterCommands() { RegisterUser = request };
            var response = await _mediator.Send(command);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        [MapToApiVersion(1)]
        [HttpPost("Login")]
        public async Task<ActionResult<CustomResult<Guid>>> login([FromBody] LoginDto request)
        {

            var command = new LoginCommands() { payload = request };
            var response = await _mediator.Send(command);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

    }
}
