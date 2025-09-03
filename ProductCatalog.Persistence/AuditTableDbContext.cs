using Microsoft.EntityFrameworkCore;
using ProductCatalog.Dormain.Common;


namespace ProductCatalog.Persistence
{
    public class AuditTableDbContext:DbContext
    {
        public AuditTableDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual async Task<int> SaveChangesAsync(string username)
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseDormainEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.DateUpdated = DateTime.Now.Date;
                    entry.Entity.UpdatedBy = username;
                }

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now.Date;
                    entry.Entity.CreatedBy = username;
                }
            }

            var result = await base.SaveChangesAsync();

            return result;
        }
    }
}
