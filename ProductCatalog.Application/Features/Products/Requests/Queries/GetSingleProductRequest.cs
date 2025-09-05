using MediatR;
using ProductCatalog.Application.Dtos.Products;
using ProductCatalog.Application.Responses;


namespace ProductCatalog.Application.Features.Products.Requests.Queries
{
    public class GetSingleProductRequest:IRequest<CustomResult<GetProductDto>>
    {
        public Guid ProductId { get; set; }    
    }
}
