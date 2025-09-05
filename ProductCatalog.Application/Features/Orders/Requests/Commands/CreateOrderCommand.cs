using MediatR;
using ProductCatalog.Application.Dtos.Orders;
using ProductCatalog.Application.Models;
using ProductCatalog.Application.Responses;


namespace ProductCatalog.Application.Features.Orders.Requests.Commands
{
    public class CreateOrderCommand:IRequest<CustomResult<OrderResponse>>
    {
        public CreateOrderDto? CreateOrderDto { get; set; }
    }
}
