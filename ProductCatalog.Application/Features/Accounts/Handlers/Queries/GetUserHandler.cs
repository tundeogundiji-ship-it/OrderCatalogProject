using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using ProductCatalog.Application.Constants;
using ProductCatalog.Application.Contracts.Authentication;
using ProductCatalog.Application.Contracts.Repository;
using ProductCatalog.Application.Dtos.Authentication;
using ProductCatalog.Application.Features.Accounts.Requests.Queries;
using ProductCatalog.Application.Responses;
using System.Security.Claims;


namespace ProductCatalog.Application.Features.Accounts.Handlers.Queries
{
    public class GetUserHandler : IRequestHandler<GetUserDetailRequest, CustomResult<GetUsersDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserContext userContext;
        public GetUserHandler(IUnitOfWork unitOfWork,IMapper mapper,IUserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            this.userContext = userContext;
        }

        public async Task<CustomResult<GetUsersDto>> Handle(GetUserDetailRequest request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.authRepository.GetUser(userContext.GetUserId());
            GetUsersDto userDto = _mapper.Map<GetUsersDto>(user);
            return CustomResult<GetUsersDto>.Success(userDto);
        }
    }
}
