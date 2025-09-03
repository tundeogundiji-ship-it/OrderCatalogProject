

namespace ProductCatalog.Application.Contracts.Repository
{
    public interface IUnitOfWork:IDisposable
    {
        Task Save();
        IProductRepository productRepository { get; }
        IAuthRepository authRepository { get; }
        IOrderRepository orderRepository { get; }
    }
}
