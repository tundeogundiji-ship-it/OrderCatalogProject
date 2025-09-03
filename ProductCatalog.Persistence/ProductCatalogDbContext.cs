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

        public async Task<int> Save(string username)
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseDormainEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.DateUpdated = DateTime.Now.Date;
                    entry.Entity.UpdatedBy = username!;
                }

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now.Date;
                    entry.Entity.CreatedBy = username!;
                }
            }

            var result = await base.SaveChangesAsync();

            return result;
        }

    }
}
