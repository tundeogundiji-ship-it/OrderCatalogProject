using FluentValidation;
using System.Text.RegularExpressions;


namespace ProductCatalog.Application.Dtos.Products.Validators
{
    public class IProductValidator:AbstractValidator<IProductDto>
    {
        public IProductValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("{PropertyName} is required")
               .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 character of length")
               .Must(ContainOnlyAlphabet).WithMessage("{PropertyName} can not contain digit or special characters")
               .NotNull();

            RuleFor(x => x.Description)
               .NotEmpty().WithMessage("{PropertyName} is required")
               .MaximumLength(150).WithMessage("{PropertyName} must not exceed 50 character of length")
               .NotNull();

            RuleFor(x => x.Price)
               .NotEmpty().WithMessage("{PropertyName}  is required")
               .NotNull();

            RuleFor(x => x.StockQuantity)
               .GreaterThan(0).WithMessage("{PropertyName} is not valid")
               .NotNull();
        }

        public static bool ContainOnlyAlphabet(string input)
        {
            return Regex.IsMatch(input, "^[a-zA-Z]+$");
        }
    }
}
