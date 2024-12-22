using DashBoard.Infrastructure.APIClients.Abstractions;
using DashBoard.Infrastructure.APIClients.Implementations;
using DashBoard.Infrastructure.Repositories;
using DashBoard.Services.Abstractions;
using DashBoard.Services.Implementations;
using E_Commerce.Domain.Interfaces;
using E_Commerce.Infastructure.Abstraction;
using E_Commerce.Infastructure.Repositories;

namespace DashBoard.Infrastructure.Extensions
{
    public static class DashModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenaricRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IDashboardProductService, DashboardProductService>();
            services.AddScoped<IDashboardProductService, DashboardProductService>();
            services.AddSingleton<IApiClient, ApiClient>();
            

            return services;
        }

    }
}
