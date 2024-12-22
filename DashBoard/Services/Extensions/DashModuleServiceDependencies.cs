using DashBoard.Infrastructure.APIClients.Abstractions;
using DashBoard.Infrastructure.APIClients.Implementations;
using DashBoard.Infrastructure.Repositories;
using DashBoard.Services.Abstractions;
using DashBoard.Services.Implementations;
using E_Commerce.Domain.Interfaces;
using E_Commerce.Infastructure.Repositories;
using E_Commerce.Services.Abstraction.IApplicationUserServices;
using E_Commerce.Services.Abstraction.IAuthenticationServices;
using E_Commerce.Services.Abstraction.ICategoryServices;
using E_Commerce.Services.Abstraction.IEmailServices;
using E_Commerce.Services.Abstraction.IReviewServices;
using E_Commerce.Services.Abstraction.OrderService;
using E_Commerce.Services.Abstraction.ProductService;
using E_Commerce.Services.Abstraction.ShoppingCartItemService;
using E_Commerce.Services.Abstraction.ShoppingCartService;
using E_Commerce.Services.Implemantation.ApplicationUserService;
using E_Commerce.Services.Implemantation.AuthanticationService;
using E_Commerce.Services.Implemantation.CategoryService;
using E_Commerce.Services.Implemantation.EmailServices;
using E_Commerce.Services.Implemantation.OrderService;
using E_Commerce.Services.Implemantation.ProductService;
using E_Commerce.Services.Implemantation.ReviewService;
using E_Commerce.Services.Implemantation.ShoppingCartItemService;
using E_Commerce.Services.Implemantation.ShoppingCartServices;

namespace DashBoard.Services.Extensions
{
    public static class DashModuleServiceDependencies
    {

        public static IServiceCollection AddServiceDependendcies(this IServiceCollection services)
        {

            services.AddTransient(typeof(IGenaricRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IDashboardProductService, DashboardProductService>();
            services.AddTransient<IApiClient, ApiClient>();
           


            return services;
        }
    }
}
