using AutoMapper;
using MediatR;
using ProductCatalog.Application.Contracts.Repository;
using ProductCatalog.Application.Dtos.Products;
using ProductCatalog.Application.Features.Products.Requests.Queries;
using ProductCatalog.Application.Responses;


namespace ProductCatalog.Application.Features.Products.Handlers.Queries
{
    public class GetProductsHandler : IRequestHandler<GetProductRequestList, CustomResult<IEnumerable<GetProductDto>>>
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IMapper _mapper;
        public GetProductsHandler(IUnitOfWork unitofWork,IMapper mapper)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
        }
        public async Task<CustomResult<IEnumerable<GetProductDto>>> Handle(GetProductRequestList request, CancellationToken cancellationToken)
        {
            var products = await _unitofWork.productRepository.GetProducts();
            IEnumerable<GetProductDto> productDto = _mapper.Map<IEnumerable<GetProductDto>>(products);

            return CustomResult<IEnumerable<GetProductDto>>.Success(productDto);
        }
    }
}
