using Microsoft.Extensions.DependencyInjection;
using StudentTracking.Business.Interfaces;
using StudentTracking.Core;
using StudentTracking.Data.EntityFramework.Repositories.Interfaces;
using StudentTracking.Data.EntityFramework.UnitOfWork;

namespace ELECTRACORE.Business.Utilities.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();

            services.AddHttpClient();

            services.Scan(scan => scan
           .FromAssemblies(typeof(IUserRepository).Assembly)
           .AddClasses(classes => classes.AssignableTo<IScopedRepository>(), publicOnly: false)
           .AsImplementedInterfaces()
           .WithScopedLifetime());

            services.Scan(scan => scan
           .FromAssemblies(typeof(IAuthenticationService).Assembly)
           .AddClasses(classes => classes.AssignableTo<IScopedService>(), publicOnly: false)
           .AsImplementedInterfaces()
           .WithScopedLifetime());

            return services;
        }
    }
}
