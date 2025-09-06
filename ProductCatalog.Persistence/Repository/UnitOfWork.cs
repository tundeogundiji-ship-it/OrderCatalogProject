using ProductCatalog.Application.Contracts.Authentication;
using ProductCatalog.Application.Contracts.Repository;

namespace ProductCatalog.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductCatalogDbContext _context;
        private IProductRepository? _productRepository;
        private IOrderRepository? _orderRepository;
        private IAuthRepository? _authRepository;
        private readonly IUserContext userContext;

        public UnitOfWork(ProductCatalogDbContext context,IUserContext userContext)
        {
            _context = context;
            this.userContext = userContext;
        }

        public IProductRepository productRepository => _productRepository ??= new ProductRepository(_context);

        public IAuthRepository authRepository => _authRepository ??= new AuthRepository(_context);

        public IOrderRepository orderRepository => _orderRepository ??= new OrderRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Save()
        {
            var username = userContext.GetUserName();
            await _context.SaveChangesAsync(username!);
        }
    }
}
