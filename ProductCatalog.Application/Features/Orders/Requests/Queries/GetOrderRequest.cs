using MediatR;
using ProductCatalog.Application.Dtos.Orders;
using ProductCatalog.Application.Responses;

namespace ProductCatalog.Application.Features.Orders.Requests.Queries
{
    public class GetOrderRequest:IRequest<CustomResult<GetOrderDto>>
    {
        public Guid orderId { get; set; }
    }
}
