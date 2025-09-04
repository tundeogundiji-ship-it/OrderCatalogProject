using FluentValidation;
using ProductCatalog.Application.Contracts.Repository;


namespace ProductCatalog.Application.Dtos.Products.Validators
{
    public class CreateProductValidator:AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Include(new IProductValidator());

            RuleFor(e => e)
            .MustAsync(ProductNameUnique)
            .WithMessage("product name exists");

        }
        public IUnitOfWork _unitOfWork;

        private async Task<bool> ProductNameUnique(CreateProductDto e, CancellationToken cancellationToken)
        {
            return ! await _unitOfWork.productRepository.IsProductExist(e.Name!);
        }
    }
}
