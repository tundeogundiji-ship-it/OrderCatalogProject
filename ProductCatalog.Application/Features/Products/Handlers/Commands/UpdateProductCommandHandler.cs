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
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, CustomResult<ProductResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductCommandHandler> logger;
        public UpdateProductCommandHandler(IUnitOfWork unitOfWork,IMapper mapper, ILogger<CreateProductCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            this.logger = logger;
        }
        public async Task<CustomResult<ProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var response = new ProductResponse();

            //validate request
            logger.LogInformation("Update Product request is {@request}", JsonConvert.SerializeObject(request));

            StringBuilder error = new StringBuilder();

            var validators = new UpdateProductValidator();
            var validationResult = await validators.ValidateAsync(request.UpdateProductDto!);

            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    error.Append(item);
                }

                logger.LogError("Validation error with message {@message}", error.ToString());

                return CustomResult<ProductResponse>.Failure(CustomError.ValidationError(error.ToString()));
            }

            Product? product = await _unitOfWork.productRepository.GetProduct(request.UpdateProductDto!.Id);

            if (product is null)
            {
                var message = $"product record with Id => {request.UpdateProductDto!.Id} not found";

                logger.LogError("product with Id {@productId} could not be found", request.UpdateProductDto!.Id);

                return CustomResult<ProductResponse>.Failure(CustomError.RecordNotFound(message));

            }

            _mapper.Map(request.UpdateProductDto, product);
            _unitOfWork.productRepository.Update(product);

            await _unitOfWork.Save();

            response.message = ResponseMessageConstant.UpdateProductMessage;

            return CustomResult<ProductResponse>.Success(response);
        }
    }
}
