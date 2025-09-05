using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProductCatalog.Application.Constants;
using ProductCatalog.Application.Contracts.Repository;
using ProductCatalog.Application.Dtos.Orders.Validators;
using ProductCatalog.Application.Features.Orders.Requests.Commands;
using ProductCatalog.Application.Models;
using ProductCatalog.Application.Responses;
using ProductCatalog.Dormain;
using System.Text;


namespace ProductCatalog.Application.Features.Orders.Handlers.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CustomResult<OrderResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateOrderCommandHandler> logger;
        private readonly IHttpContextAccessor httpContextAccessor;
        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,
            ILogger<CreateOrderCommandHandler> logger,IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            this.logger = logger;
            this.httpContextAccessor = httpContextAccessor;
        }
        
        
        public async Task<CustomResult<OrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var response = new OrderResponse();
            string? userId = httpContextAccessor.HttpContext!.User.FindFirst(CustomClaimTypes.Uid)?.Value;
            //validation request
            logger.LogInformation("Create Order request is {@request}", JsonConvert.SerializeObject(request));

            StringBuilder error = new StringBuilder();
            var validators = new CreateOrderValidators(_unitOfWork);
            var validationResult = await validators.ValidateAsync(request.CreateOrderDto!);

            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    error.Append(item);
                }

                logger.LogError("Validation error with message {@message}", error.ToString());

                return CustomResult<OrderResponse>.Failure(CustomError.ValidationError(error.ToString()));
            }

           
            //map input
            Order? orderRequest = _mapper.Map<Order>(request.CreateOrderDto!);
            orderRequest.DateCreated = DateTime.Now;
            orderRequest.CreatedBy = userId;
            orderRequest.OrderDate = DateTime.Now;
            orderRequest.UserId = Guid.Parse(userId!);
            orderRequest.TotalAmount = request.CreateOrderDto!.OrderItems!.Sum(x=>x.UnitPrice);

            //add order
            response = await _unitOfWork.orderRepository.CreateOrder(orderRequest);

            if (response.message!=ResponseMessageConstant.SuccessfulOrderMessage)
            {
                return CustomResult<OrderResponse>.Failure(CustomError.OrderFailed(response.
                    message!));
            }

            return CustomResult<OrderResponse>.Success(response);
            
        }
    }
}
