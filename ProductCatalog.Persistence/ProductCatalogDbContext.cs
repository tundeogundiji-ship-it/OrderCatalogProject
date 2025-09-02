using Microsoft.EntityFrameworkCore;
using ProductCatalog.Dormain;

namespace ProductCatalog.Persistence
{
    public class ProductCatalogDbContext:DbContext
    {
        public ProductCatalogDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
