using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using ProductCatalog.Application.Constants;
using ProductCatalog.Application.Contracts.Authentication;
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
        private readonly IUserContext userContext;
        public GetOrdersListHandler(IUnitOfWork unitOfWork,IMapper mapper,
            IUserContext userContext
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            this.userContext = userContext;
        }
        public async Task<CustomResult<IEnumerable<GetOrderDto>>> Handle(GetOrdersRequest request, CancellationToken cancellationToken)
        {
            Guid userId = userContext.GetUserId();
            IEnumerable<Order>? orders = await _unitOfWork.orderRepository.GetAllOrder(userId);
            var orderDto = _mapper.Map<IEnumerable<GetOrderDto>>(orders);

            return CustomResult<IEnumerable<GetOrderDto>>.Success(orderDto);
        }
    }
}
