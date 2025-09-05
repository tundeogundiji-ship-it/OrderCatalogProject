using AutoMapper;
using MediatR;
using ProductCatalog.Application.Contracts.Repository;
using ProductCatalog.Application.Dtos.Authentication;
using ProductCatalog.Application.Features.Accounts.Requests.Queries;
using ProductCatalog.Application.Responses;

namespace ProductCatalog.Application.Features.Accounts.Handlers.Queries
{
    public class GetUsersHandler:IRequestHandler<GetUserRequestList,CustomResult<IEnumerable<GetUsersDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetUsersHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomResult<IEnumerable<GetUsersDto>>> Handle(GetUserRequestList request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.authRepository.GetUsers();
            IEnumerable<GetUsersDto> userDto = _mapper.Map<IEnumerable<GetUsersDto>>(users);
            return CustomResult<IEnumerable<GetUsersDto>>.Success(userDto);
        }
    }
}
