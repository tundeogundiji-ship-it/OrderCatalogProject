using MediatR;
using ProductCatalog.Application.Dtos.Products;
using ProductCatalog.Application.Responses;


namespace ProductCatalog.Application.Features.Products.Requests.Queries
{
    public class GetProductRequestList:IRequest<CustomResult<IEnumerable<GetProductDto>>>
    {
    }
}
