using AutoMapper;
using MediatR;
using ProductCatalog.Application.Contracts.Repository;
using ProductCatalog.Application.Dtos.Authentication;
using ProductCatalog.Application.Features.Accounts.Requests.Queries;
using ProductCatalog.Application.Responses;


namespace ProductCatalog.Application.Features.Accounts.Handlers.Queries
{
    public class GetUserHandler : IRequestHandler<GetUserDetailRequest, CustomResult<GetUsersDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetUserHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CustomResult<GetUsersDto>> Handle(GetUserDetailRequest request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.authRepository.GetUser(request.userId);
            GetUsersDto userDto = _mapper.Map<GetUsersDto>(user);
            return CustomResult<GetUsersDto>.Success(userDto);
        }
    }
}
