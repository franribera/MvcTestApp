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
using MvcTestApp.Authentication;
using MvcTestApp.Domain.Users;
using MvcTestApp.Extensions;
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
                    options.AccessDeniedPath = new PathString("/login/Unauthorize");
                    options.Cookie.Name = "MvcTestApp";
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                    options.SlidingExpiration = true;
                })
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(BasicAuthenticationDefaults.AuthenticationScheme, null);

            services
                .RegisterInfrastructure()
                .RegisterApplication()
                .RegisterWeb();
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
