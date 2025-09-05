using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ProductCatalog.Application.Constants;
using ProductCatalog.Application.Contracts.Authentication;
using ProductCatalog.Application.Contracts.Repository;
using ProductCatalog.Application.Dtos.Authentication.Validators;
using ProductCatalog.Application.Features.Accounts.Requests.Commands;
using ProductCatalog.Application.Models;
using ProductCatalog.Application.Responses;
using ProductCatalog.Dormain;
using System.Text;


namespace ProductCatalog.Application.Features.Accounts.Handlers.Commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommands, CustomResult<RegisterUserResponse>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<RegisterCommandHandler> _logger;
        private readonly IPasswordHasher _passwordHasher;
        public RegisterCommandHandler(IUnitOfWork unitOfWork,IPasswordHasher passwordHasher,
            IMapper mapper, ILogger<RegisterCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        public async Task<CustomResult<RegisterUserResponse>> Handle(RegisterCommands request, CancellationToken cancellationToken)
        {
            var response = new RegisterUserResponse();
            StringBuilder error = new StringBuilder();

            //validation of request

            var validators = new RegisterValidators(_unitOfWork);
            var validationResult = await validators.ValidateAsync(request.RegisterUser!);

            if (validationResult.IsValid == false)
            {
                foreach (var item in validationResult.Errors)
                {
                    error.Append(item);
                }

                _logger.LogError("Validation error with message {@message}", error.ToString());

                return CustomResult<RegisterUserResponse>.Failure(CustomError.ValidationError(error.ToString()));
            }

            //map record from the dto to user entity
            User user = _mapper.Map<User>(request.RegisterUser);
            user.PasswordHash = _passwordHasher.Hash(request.RegisterUser!.Password!);

            var userEntity = await _unitOfWork.authRepository.Add(user);
            await _unitOfWork.SaveChanges();

            response.message = ResponseMessageConstant.RegisterUserSuccessMessage;

            return CustomResult<RegisterUserResponse>.Success(response);

        }
    }
}
