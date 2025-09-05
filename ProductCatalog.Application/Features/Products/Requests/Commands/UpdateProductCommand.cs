using MediatR;
using ProductCatalog.Application.Dtos.Products;
using ProductCatalog.Application.Models;
using ProductCatalog.Application.Responses;


namespace ProductCatalog.Application.Features.Products.Requests.Commands
{
    public class UpdateProductCommand:IRequest<CustomResult<ProductResponse>>
    {
        public UpdateProductDto? UpdateProductDto { get; set; }
    }
}
