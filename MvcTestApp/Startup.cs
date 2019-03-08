using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MvcTestApp.Application.Commands.Users;
using MvcTestApp.Application.Commands.Users.Create;
using MvcTestApp.Application.Commands.Users.Delete;
using MvcTestApp.Application.Commands.Users.Update;
using MvcTestApp.Application.Queries.Users;
using MvcTestApp.Domain.Users;
using MvcTestApp.Infrastructure;
using MvcTestApp.Middlewares;
using MvcTestApp.Presenters.Users;
using AuthenticationService = MvcTestApp.Authentication.AuthenticationService;
using IAuthenticationService = MvcTestApp.Authentication.IAuthenticationService;

namespace MvcTestApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc(config =>
                    {
                        config.RespectBrowserAcceptHeader = true;
                        config.ReturnHttpNotAcceptable = true;
                        config.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                        config.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                    }
                )
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/login/login");
                    options.AccessDeniedPath = new PathString("/login/login");
                    options.Cookie.Name = "MvcTestApp";
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                    options.SlidingExpiration = true;
                });

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
            services.AddScoped<ICreateUserPresenter, CreateUserPresenter>();
            services.AddScoped<IUserFactory, UserFactory>();

            services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
            services.AddScoped<IDeleteUserPresenter, DeleteUserPresenter>();

            services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            services.AddScoped<IUpdateUserPresenter, UpdateUserPresenter>();

            services.AddScoped<IUserQueries, UserQueries>();
            services.AddSingleton<IUserRepository, UserRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionHandler>();

            app.UseMvc();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
