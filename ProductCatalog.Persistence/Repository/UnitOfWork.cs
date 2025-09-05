using Microsoft.AspNetCore.Http;
using ProductCatalog.Application.Constants;
using ProductCatalog.Application.Contracts.Repository;

namespace ProductCatalog.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductCatalogDbContext _context;
        private IProductRepository? _productRepository;
        private IOrderRepository? _orderRepository;
        private IAuthRepository? _authRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UnitOfWork(ProductCatalogDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
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
            var username = _httpContextAccessor.HttpContext!.User.FindFirst(CustomClaimTypes.Uid)?.Value;
            await _context.SaveChangesAsync(username!);
        }
    }
}
