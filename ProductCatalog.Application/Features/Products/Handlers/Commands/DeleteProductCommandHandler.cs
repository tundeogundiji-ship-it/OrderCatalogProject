using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProductCatalog.Application.Constants;
using ProductCatalog.Application.Contracts.Repository;
using ProductCatalog.Application.Features.Products.Requests.Commands;
using ProductCatalog.Application.Models;
using ProductCatalog.Application.Responses;
using System;
namespace ProductCatalog.Application.Features.Products.Handlers.Commands
{
    public class DeleteProductCommandHandler:IRequestHandler<DeleteProductCommand,CustomResult<ProductResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteProductCommandHandler> _logger;
        public DeleteProductCommandHandler(IUnitOfWork unitOfWork,ILogger<DeleteProductCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<CustomResult<ProductResponse>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var response = new ProductResponse();
            _logger.LogInformation("Delete product request is {@request}", JsonConvert.SerializeObject(request));

            var product = await _unitOfWork.productRepository.GetProduct(request.ProductId);
            if (product is null)
            {
                _logger.LogError("Record with productId {@productId} not found", request.ProductId);

                var message = $"product record with Id => {request.ProductId} not found";

                return CustomResult<ProductResponse>.Failure(CustomError.RecordNotFound(message));
            }

            product.IsActive = false;
            _unitOfWork.productRepository.Update(product);
            await _unitOfWork.Save();

            response.message = ResponseMessageConstant.DeleteProductMessage;

            return CustomResult<ProductResponse>.Success(response);



        }
    }
}
