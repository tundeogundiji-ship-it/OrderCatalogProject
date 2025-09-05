using MediatR;
using Microsoft.Extensions.Logging;
using ProductCatalog.Application.Constants;
using ProductCatalog.Application.Contracts.Authentication;
using ProductCatalog.Application.Contracts.Repository;
using ProductCatalog.Application.Dtos.Authentication.Validators;
using ProductCatalog.Application.Features.Accounts.Requests.Commands;
using ProductCatalog.Application.Models;
using ProductCatalog.Application.Responses;
using System.Text;

namespace ProductCatalog.Application.Features.Accounts.Handlers.Commands
{
    public class LoginUserCommandHandler : IRequestHandler<LoginCommands, CustomResult<LoginUserResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<LoginUserCommandHandler> _logger;
        private readonly ITokenProvider tokenProvider;
        private readonly IPasswordHasher passwordHasher;

        public LoginUserCommandHandler(IUnitOfWork unitOfWork,IPasswordHasher passwordHasher,
            ITokenProvider tokenProvider, ILogger<LoginUserCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            this.tokenProvider = tokenProvider;
            _logger = logger;
            this.passwordHasher = passwordHasher;
        }
        public async Task<CustomResult<LoginUserResponse>> Handle(LoginCommands request, CancellationToken cancellationToken)
        {
            var loginResponse = new LoginUserResponse();

            StringBuilder error = new StringBuilder();
            //validation of request

            var validators = new LoginValidators();
            var validationResult = await validators.ValidateAsync(request.payload!);

            if (validationResult.IsValid == false)
            {
                foreach (var item in validationResult.Errors)
                {
                    error.Append(item);
                }

                _logger.LogError("Validation error with message {@message}", error.ToString());

                return CustomResult<LoginUserResponse>.Failure(CustomError.ValidationError(error.ToString()));
            }

            // get the user by email
            var user = await _unitOfWork.authRepository.GetUserByEmail(request.payload!.Email!);

            if(user is null)
            {
                _logger.LogError("User with account {@user} is not found", request.payload.Email);
                return CustomResult<LoginUserResponse>.Failure(CustomError.LoginFailed(ResponseMessageConstant.FailedLoginMessage));
            }

            //validate the user password entered
            bool isPasswordValid = passwordHasher.Verify(request.payload!.Password!, user.PasswordHash!);

            if (!isPasswordValid)
            {
                _logger.LogError("Invalid credentials entered by user with email {@email}",request.payload.Email);
                return CustomResult<LoginUserResponse>.Failure(CustomError.LoginFailed(ResponseMessageConstant.FailedLoginMessage));
            }

            //user is valid generate jwt token
            string jwtToken = tokenProvider.GenerateJwtToken(user);
            loginResponse.message = ResponseMessageConstant.SuccessfulLoginMessage;
            loginResponse.token = jwtToken;

            return CustomResult<LoginUserResponse>.Success(loginResponse);

        }
    }
}
