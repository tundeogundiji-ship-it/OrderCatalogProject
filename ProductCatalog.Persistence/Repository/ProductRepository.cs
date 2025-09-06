using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Contracts.Repository;
using ProductCatalog.Dormain;


namespace ProductCatalog.Persistence.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ProductCatalogDbContext _dbContext;
        public ProductRepository(ProductCatalogDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product?> GetProduct(Guid productId)
        {
            Product? product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == productId && x.IsActive);
            return product;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            IEnumerable<Product> products = Enumerable.Empty<Product>();
            products = await _dbContext.Products.Where(x=>x.IsActive).ToListAsync();

            return products;
        }

        public async Task<bool> IsProductExist(string name)
        {
            bool IsAvailable = false;
            IsAvailable = await _dbContext.Products.AnyAsync(x => x.Name!.ToLower() == name.ToLower() && x.IsActive);

            return IsAvailable;
        }
    }
}
