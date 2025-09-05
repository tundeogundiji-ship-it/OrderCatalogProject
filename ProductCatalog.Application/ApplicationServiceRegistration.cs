using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Application.Profiles;
using System.Reflection;


namespace ProductCatalog.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
