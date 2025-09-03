using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Application.Contracts.Repository;
using ProductCatalog.Persistence.Repository;


namespace ProductCatalog.Persistence
{
    public static class PersistenceServiceConfiguration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlServer<ProductCatalogDbContext>(configuration.GetConnectionString("DefaultConnection"));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;

        }
    }
}
