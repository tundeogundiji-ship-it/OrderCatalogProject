using MediatR;
using ProductCatalog.Application.Dtos.Authentication;
using ProductCatalog.Application.Responses;


namespace ProductCatalog.Application.Features.Accounts.Requests.Commands
{
    public class LoginCommands:IRequest<CustomResult<Guid>>
    {
        public LoginDto? payload {  get; set; }
    }
}
