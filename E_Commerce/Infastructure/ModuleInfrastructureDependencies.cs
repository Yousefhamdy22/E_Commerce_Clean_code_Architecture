using E_Commerce.Domain.Interfaces;
using E_Commerce.Infastructure.Abstraction;
using E_Commerce.Infastructure.Repositories;

namespace E_Commerce.Infastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(GenaricRepo<>));
            services.AddTransient<ICategory, CategoryRepo>();
            services.AddTransient<IProduct, ProductRepo>();
            services.AddTransient<ICart, CartRepo>();
            services.AddTransient<IOrder, OrderRepo>();
            services.AddTransient<IReview, ReviewRepo>();

            return services;
        }
    }
}
