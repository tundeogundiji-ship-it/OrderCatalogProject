using FluentValidation;
using ProductCatalog.Application.Contracts.Repository;



namespace ProductCatalog.Application.Dtos.Orders.Validators
{
    public class CreateOrderValidators:AbstractValidator<CreateOrderDto>
    {
        public CreateOrderValidators(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleForEach(x => x.OrderItems).SetValidator(new OrderItemValidators(_unitOfWork));
        }

        public IUnitOfWork _unitOfWork;
    }
}
