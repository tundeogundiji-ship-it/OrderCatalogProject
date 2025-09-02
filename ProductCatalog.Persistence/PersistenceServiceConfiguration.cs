using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace ProductCatalog.Persistence
{
    public static class PersistenceServiceConfiguration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlServer<ProductCatalogDbContext>(configuration.GetConnectionString("DefaultConnection"));
            return services;

        }
    }
}
