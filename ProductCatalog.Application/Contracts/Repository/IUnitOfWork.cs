

namespace ProductCatalog.Application.Contracts.Repository
{
    public interface IUnitOfWork:IDisposable
    {
        Task Save();
        Task SaveChanges();
        IProductRepository productRepository { get; }
        IAuthRepository authRepository { get; }
        IOrderRepository orderRepository { get; }
    }
}
