using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProductCatalog.Application.Constants;
using ProductCatalog.Application.Contracts.Authentication;
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
        private readonly IUserContext userContext;
        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,
            ILogger<CreateOrderCommandHandler> logger,IUserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            this.logger = logger;
            this.userContext = userContext;
        }
        
        
        public async Task<CustomResult<OrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var response = new OrderResponse();
            string username = userContext.GetUserName();
            Guid userId = userContext.GetUserId();
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
            orderRequest.CreatedBy = username;
            orderRequest.OrderDate = DateTime.Now;
            orderRequest.UserId = userId;
            
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
