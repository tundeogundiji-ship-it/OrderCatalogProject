using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Dormain;
using ProductCatalog.Dormain.Common;
using System.Security.Claims;

namespace ProductCatalog.Persistence
{
    public class ProductCatalogDbContext:AuditTableDbContext
    {
        
        public ProductCatalogDbContext(DbContextOptions<ProductCatalogDbContext> options) : base(options)
        {
        }

      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductCatalogDbContext).Assembly);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> Users { get; set; }

       

    }
}
