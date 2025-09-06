using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProductCatalog.Application.Contracts.Authentication;
using ProductCatalog.Application.Contracts.Repository;
using ProductCatalog.Persistence.Authentication;
using ProductCatalog.Persistence.Repository;
using System.Text;
using System.Text.Json;


namespace ProductCatalog.Persistence
{
    public static class PersistenceServiceConfiguration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlServer<ProductCatalogDbContext>(configuration.GetConnectionString("DefaultConnection"));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<ITokenProvider, TokenProvider>();
            services.AddTransient<IUserContext, UserContext>();
            services.AddHttpContextAccessor();

          

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(o =>
              {
                  o.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ClockSkew = TimeSpan.Zero,
                      ValidIssuer = configuration["JwtSettings:Issuer"],
                      ValidAudience = configuration["JwtSettings:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!))
                  };

                  o.Events = new JwtBearerEvents
                  {
                      OnChallenge = context =>
                      {
                          // Suppress the default WWW-Authenticate header
                          context.HandleResponse();

                          var problemDetails = new ProblemDetails
                          {
                              Status = StatusCodes.Status401Unauthorized,
                              Title = "Unauthorized",
                              Type = "https://httpstatuses.com/401",
                              Detail = "Access token is missing or invalid.",
                              Instance = context.Request.Path
                          };

                          var json = JsonSerializer.Serialize(problemDetails);
                          context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                          context.Response.ContentType = "application/problem+json";

                          return context.Response.WriteAsync(json);
                      },
                      OnForbidden = context =>
                      {
                          var problemDetails = new ProblemDetails
                          {
                              Status = StatusCodes.Status403Forbidden,
                              Title = "Forbidden",
                              Type = "https://httpstatuses.com/403",
                              Detail = "You do not have permission to access this resource.",
                              Instance = context.Request.Path
                          };

                          var json = JsonSerializer.Serialize(problemDetails);
                          context.Response.StatusCode = StatusCodes.Status403Forbidden;
                          context.Response.ContentType = "application/problem+json";

                          return context.Response.WriteAsync(json);
                      }
                  };
              });

            return services;

        }
    }
}
