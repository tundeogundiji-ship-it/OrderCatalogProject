using MediatR;
using ProductCatalog.Application.Models;
using ProductCatalog.Application.Responses;


namespace ProductCatalog.Application.Features.Products.Requests.Commands
{
    public class DeleteProductCommand:IRequest<CustomResult<ProductResponse>>
    {
        public Guid ProductId { get; set; }
    }
}
