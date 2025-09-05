using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProductCatalog.Application.Constants;
using ProductCatalog.Application.Contracts.Repository;
using ProductCatalog.Application.Dtos.Products.Validators;
using ProductCatalog.Application.Features.Products.Requests.Commands;
using ProductCatalog.Application.Models;
using ProductCatalog.Application.Responses;
using ProductCatalog.Dormain;
using System.Text;


namespace ProductCatalog.Application.Features.Products.Handlers.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CustomResult<ProductResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductCommandHandler> logger;
        public CreateProductCommandHandler(IUnitOfWork unitOfWork,IMapper mapper,
            ILogger<CreateProductCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            this.logger = logger;
        }
        public async Task<CustomResult<ProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var response = new ProductResponse();
            //validation request
            logger.LogInformation("Create Product request is {@request}", JsonConvert.SerializeObject(request));

            StringBuilder error = new StringBuilder();
            var validators = new CreateProductValidator(_unitOfWork);
            var validationResult = await validators.ValidateAsync(request.CreateProductDto!);

            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    error.Append(item);
                }

                logger.LogError("Validation error with message {@message}", error.ToString());

                return CustomResult<ProductResponse>.Failure(CustomError.ValidationError(error.ToString()));
            }

            Product? productRequest = _mapper.Map<Product>(request.CreateProductDto!);
            productRequest.IsActive = true;

            Product entity = await _unitOfWork.productRepository.Add(productRequest);
            await _unitOfWork.Save();

            response.message = ResponseMessageConstant.SuccessfulProductMessage;
            response.productId = entity.Id;
            

            logger.LogInformation("Creation of product successful");

            return CustomResult<ProductResponse>.Success(response);

        }
    }
}
