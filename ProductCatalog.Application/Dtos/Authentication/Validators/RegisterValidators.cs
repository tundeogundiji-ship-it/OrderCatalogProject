using FluentValidation;
using ProductCatalog.Application.Contracts.Repository;
using System.Text.RegularExpressions;

namespace ProductCatalog.Application.Dtos.Authentication.Validators
{
    public class RegisterValidators:AbstractValidator<RegisterUserDto>
    {
        public RegisterValidators(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(p => p.FirstName!)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .Must(ContainOnlyAlphabet).WithMessage("{PropertyName} should not contain special character")
            .NotNull();

            RuleFor(p => p.LastName!)
               .NotEmpty().WithMessage("{PropertyName} is required")
               .Must(ContainOnlyAlphabet).WithMessage("{PropertyName} should not contain special character")
               .NotNull();

            RuleFor(p => p.Password)
                    .NotEmpty().WithMessage("Your password cannot be empty")
                    .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                    .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                    .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                    .Matches(@"[^A-Za-z0-9]").WithMessage("Your password must contain at least special character");


            RuleFor(x => x.Email!)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .EmailAddress().WithMessage("{PropertyName} is not valid")
                .NotNull();

            RuleFor(x => x)
               .MustAsync(IsAccountExist).WithMessage("Account exist");

        }

        

        public IUnitOfWork _unitOfWork;

        private async Task<bool> IsAccountExist(RegisterUserDto e, CancellationToken token)
        {
            bool isExist = false;
            isExist =  await _unitOfWork.authRepository.IsUserExist(e.Email!);
            return isExist;
        }

        public static bool ContainOnlyAlphabet(string input)
        {
            return Regex.IsMatch(input, "^[a-zA-Z]+$");
        }
    }
}
