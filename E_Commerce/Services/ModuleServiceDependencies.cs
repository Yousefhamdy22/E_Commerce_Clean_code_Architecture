using E_Commerce.Domain.Interfaces;
using E_Commerce.Infastructure.Repositories;
using E_Commerce.Services.Abstraction.IApplicationUserServices;
using E_Commerce.Services.Abstraction.IAuthenticationServices;
using E_Commerce.Services.Abstraction.IAuthorizationServices;
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

namespace E_Commerce.Services
{
    public static  class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependendcies(this IServiceCollection services)
        {

            services.AddTransient(typeof(IRepository<>), typeof(GenaricRepo<>));
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryServices, CategoryService>();
            services.AddTransient<IshoppingCartServices, ShoppingCartService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IReviewServices, ReviewService>();
            services.AddTransient<IAuthenticationServices, AuthanticationService>();
            services.AddTransient<IApplicationUserServices, ApplicationUserService>();
            services.AddTransient<IshoppigCartIremService, ShoppingCartItemService>();
            services.AddTransient<IEmailServices, EmailServices>();


            return services;
        }
    }
}
