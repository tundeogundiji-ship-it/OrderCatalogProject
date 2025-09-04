using FluentValidation;

namespace ProductCatalog.Application.Dtos.Products.Validators
{
    public class UpdateProductValidator:AbstractValidator<UpdateProductDto>
    {
        public UpdateProductValidator()
        {
            Include(new IProductValidator());

            RuleFor(p => p.Id)
            .NotNull().WithMessage("{PropertyName} can not be empty")
            .NotNull();
        }
    }
}
