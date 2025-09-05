using AutoMapper;
using MediatR;
using ProductCatalog.Application.Contracts.Repository;
using ProductCatalog.Application.Dtos.Products;
using ProductCatalog.Application.Features.Products.Requests.Queries;
using ProductCatalog.Application.Responses;


namespace ProductCatalog.Application.Features.Products.Handlers.Queries
{
    public class GetSingleProductHandler : IRequestHandler<GetSingleProductRequest, CustomResult<GetProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetSingleProductHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CustomResult<GetProductDto>> Handle(GetSingleProductRequest request, CancellationToken cancellationToken)
        {
           var product = await _unitOfWork.productRepository.GetProduct(request.ProductId);
           GetProductDto productDto = _mapper.Map<GetProductDto>(product);

           return CustomResult<GetProductDto>.Success(productDto);
        }
    }
}
