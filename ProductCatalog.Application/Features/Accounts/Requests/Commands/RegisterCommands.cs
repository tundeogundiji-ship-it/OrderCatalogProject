using MediatR;
using ProductCatalog.Application.Dtos.Authentication;
using ProductCatalog.Application.Responses;

namespace ProductCatalog.Application.Features.Accounts.Requests.Commands
{
    public class RegisterCommands:IRequest<CustomResult<Guid>>
    {
        public RegisterUserDto? RegisterUser { get; set; }
    }
}
