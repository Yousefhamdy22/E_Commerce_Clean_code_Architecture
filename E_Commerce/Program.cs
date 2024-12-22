
using E_Commerce.Application.Features.ApplicationUser.Commands.Validation;
using E_Commerce.Application.Mapping;
using E_Commerce.Domain.Interfaces;
using E_Commerce.Infastructure.Data;
using E_Commerce.Infastructure.GenaricRepository;

using FluentValidation;
using Microsoft.EntityFrameworkCore;

using E_Commerce.Services;
using E_Commerce.Application.Features.ApplicationUser.Commands.Handlers;
using E_Commerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using E_Commerce.Services.Abstraction.IApplicationUserServices;
using E_Commerce.Services.Abstraction.IAuthenticationServices;
using E_Commerce.Services.Implemantation.ApplicationUserService;
using Microsoft.AspNetCore.Authentication;
using System.Reflection;
using E_Commerce.Infastructure;
using E_Commerce.Services.Implemantation.AuthanticationService;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using E_Commerce.Application.Features.ApplicationUser.Commands.Models;
using Microsoft.Extensions.Configuration;
using E_Commerce.Application.Behaivers;
using MediatR;
using E_Commerce.Application.Features.ApplicationUser;
using E_Commerce.Application.Features.Products.Query.Handler;
using E_Commerce.Services.Abstraction.IImageServices;
using E_Commerce.Services.Implemantation.ImageServices;
using E_Commerce.Infastructure.Abstraction;
using E_Commerce.Infastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using E_Commerce.Services.Implemantation.AuthorizationService;
using E_Commerce.Services.Abstraction.IAuthorizationServices;


namespace E_Commerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region ConnectionSql
            builder.Services.AddDbContext<ECommerceContext>(
              options => options.UseSqlServer(builder.Configuration.GetConnectionString("connectionsql")));
            #endregion


            builder.Services.AddInfrastructureDependencies().AddServiceDependendcies()
                   .AddCoreDependencies().AddServiceRegisteration(builder.Configuration);
            #region Depenendcy Injection
            builder.Services.AddInfrastructureDependencies().AddServiceDependendcies();
            #endregion




            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddUserCommand).Assembly));

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));



            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UserCommandHandler).Assembly));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ProductQueryHandler).Assembly));

            builder.Services.AddAutoMapper(typeof(UserCommandHandler).Assembly);

            builder.Services.AddScoped<IApplicationUserServices, ApplicationUserService>();
            builder.Services.AddTransient<IAuthenticationServices, AuthanticationService>();

            builder.Services.AddScoped<IAuthorizationServices, AuthorizationServices>();

            builder.Services.AddTransient<IAuthenticationServices, AuthanticationService>();

            builder.Services.AddTransient<IImageRepo, ImageRepo>();
            builder.Services.AddTransient<IImageServices, ImageServices>();


            builder.Services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();
            builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            builder.Services.AddHttpContextAccessor(); 

            #region Register FluentValidation
            builder.Services.AddValidatorsFromAssemblyContaining<AddUserValidator>();
            #endregion

            builder.Services.AddControllers();

          



            // Configure Identity
            //builder.Services.AddIdentity<User, IdentityRole>()
            //    .AddEntityFrameworkStores<ECommerceContext>()
            //    .AddDefaultTokenProviders();

           

         

            #region error 
            // Configuration of MediatR
             // builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            //  builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            #endregion

            #region  Logging configuration
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            #endregion

            #region Cors
            var Cors = "_cors";
            builder.Services.AddCors(options =>
              options.AddPolicy(name: Cors, builder =>

              {
                  builder.AllowAnyHeader();
                  builder.AllowAnyMethod();
                  builder.AllowAnyOrigin();
              }
                  )
            );
            #endregion

            #region MeddileWare

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(Cors);
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            #endregion

        }
    }
}
