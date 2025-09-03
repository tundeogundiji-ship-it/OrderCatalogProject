using Microsoft.EntityFrameworkCore;
using ProductCatalog.Dormain.Common;


namespace ProductCatalog.Persistence
{
    public class AuditTableDbContext:DbContext
    {
        public AuditTableDbContext(DbContextOptions options) : base(options)
        {
        }

        
    }
}
