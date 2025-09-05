using MediatR;
using ProductCatalog.Application.Dtos.Authentication;
using ProductCatalog.Application.Responses;


namespace ProductCatalog.Application.Features.Accounts.Requests.Queries
{
    public class GetUserDetailRequest:IRequest<CustomResult<GetUsersDto>>
    {
        public Guid userId { get; set; }
    }
}
