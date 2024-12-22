using FluentValidation;
using MediatR;
using System.Reflection;

namespace E_Commerce.Application.Features.ApplicationUser
{
    public static class ModuleCoreDependencies
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
        {
            // Configuration of MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Configuration of AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Register Validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Register the pipeline behavior for validation
           // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            //services.AddAutoMapper(typeof(CategoriesProfile)); // Ensure CategoriesProfile is included


            return services;
        }

    }
}
