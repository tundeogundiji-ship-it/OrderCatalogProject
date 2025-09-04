using FluentValidation;


namespace ProductCatalog.Application.Dtos.Authentication.Validators
{
    public class LoginValidators:AbstractValidator<LoginDto>
    {
        public LoginValidators()
        {
            RuleFor(x => x.Email!)
           .NotEmpty().WithMessage("{PropertyName} is required")
           .EmailAddress().WithMessage("{PropertyName} is not valid")
           .NotNull();

            RuleFor(x => x.Password!)
               .NotEmpty().WithMessage("{PropertyName} is required")
               .NotNull();
        }
    }
}
