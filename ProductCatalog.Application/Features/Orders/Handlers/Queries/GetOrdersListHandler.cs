using AutoMapper;
using MediatR;
using ProductCatalog.Application.Contracts.Repository;
using ProductCatalog.Application.Dtos.Orders;
using ProductCatalog.Application.Features.Orders.Requests.Queries;
using ProductCatalog.Application.Responses;
using ProductCatalog.Dormain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Application.Features.Orders.Handlers.Queries
{
    public class GetOrdersListHandler : IRequestHandler<GetOrdersRequest, CustomResult<IEnumerable<GetOrderDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetOrdersListHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CustomResult<IEnumerable<GetOrderDto>>> Handle(GetOrdersRequest request, CancellationToken cancellationToken)
        {
            IEnumerable<Order>? orders = await _unitOfWork.orderRepository.GetAllOrder();
            var orderDto = _mapper.Map<IEnumerable<GetOrderDto>>(orders);

            return CustomResult<IEnumerable<GetOrderDto>>.Success(orderDto);
        }
    }
}
