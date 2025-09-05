using FluentValidation;
using ProductCatalog.Application.Contracts.Repository;
using ProductCatalog.Dormain;


namespace ProductCatalog.Application.Dtos.Orders.Validators
{
    public class OrderItemValidators:AbstractValidator<OrderItemDto>
    {
        public OrderItemValidators(IUnitOfWork unitOfWork)
        {
             _unitOfWork = unitOfWork;
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull();

            RuleFor(x => x.Quantity)
               .NotEmpty().WithMessage("{PropertyName} is required")
               .GreaterThan(0).WithMessage("{PropertyName} is not valid")
               .NotNull();

            RuleFor(x => x.UnitPrice)
               .NotEmpty().WithMessage("{PropertyName} is required")
               .NotNull();

            RuleFor(e => e)
                .MustAsync(IsProductExist).WithMessage("ProductId does not exist or product does not have stock");
        }

        public IUnitOfWork _unitOfWork;

        private async Task<bool> IsProductExist(OrderItemDto e, CancellationToken cancellationToken)
        {
            bool IsExist = false;
            Product? product = await _unitOfWork.productRepository.GetProduct(e.ProductId);
            if (product is not null && product.StockQuantity>0)
            {
                IsExist = true;
            }

            return IsExist;
        }
    }
}
