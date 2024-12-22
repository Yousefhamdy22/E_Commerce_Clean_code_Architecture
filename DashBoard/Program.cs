using DashBoard.Application.Extensions;
using DashBoard.Application.Features.Products.Query.Handler;
using DashBoard.Core.Entities;
using DashBoard.Infrastructure.APIClients.Abstractions;
using DashBoard.Infrastructure.APIClients.Implementations;
using DashBoard.Infrastructure.Context;
using DashBoard.Infrastructure.Extensions;
using DashBoard.Infrastructure.Services.Abstractions;
using DashBoard.Infrastructure.Services.Implementations;
using DashBoard.Services.Abstractions;
using DashBoard.Services.Extensions;
using DashBoard.Services.Implementations;
using E_Commerce.Domain.Entities;
using E_Commerce.Infastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace DashBoard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            #region ConnectionSql
            builder.Services.AddDbContext<DashBoardContext>(
              options => options.UseSqlServer(builder.Configuration.GetConnectionString("dashConnectionsql")));
            #endregion

            //#region ConnectionSql
            //builder.Services.AddDbContext<ECommerceContext>(
            //  options => options.UseSqlServer(builder.Configuration.GetConnectionString("connectionsql")));
            //#endregion
           

            #region DI

            builder.Services.AddInfrastructureDependencies().AddServiceDependendcies()
                   .AddCoreDependencies().AddServiceRegisteration(builder.Configuration);
            #endregion


            //   builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DashAddProductCommand).Assembly));

            //   builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //   builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DashAddProductCommand).Assembly));
            //   builder.Services.AddTransient<IApiClient, ApiClient>();
            ////   Register AutoMapper
            //   builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(DashAddProductCommand).Assembly);

            builder.Services.AddScoped<IproductDashRepo, ProductDashRepo<Product>>();
            builder.Services.AddScoped<IDashboardProductService, DashboardProductService>();
            builder.Services.AddScoped<IDashboardProductService, DashboardProductService>();
      
           

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DashProductsQueryHandler).Assembly));
            builder.Services.AddHttpContextAccessor();


            #region ApiClient

            builder.Services.AddHttpClient<IApiClient, ApiClient>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            #endregion

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
