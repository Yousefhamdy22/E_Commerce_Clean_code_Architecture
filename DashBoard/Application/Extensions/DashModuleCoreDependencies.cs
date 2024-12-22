using FluentValidation;
using System.Reflection;

namespace DashBoard.Application.Extensions
{
    public static class DashModuleCoreDependencies
    {

        public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
        {
          
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Register Validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

          

            return services;
        }
    }
}
