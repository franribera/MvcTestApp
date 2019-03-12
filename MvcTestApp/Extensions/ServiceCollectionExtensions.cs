using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MvcTestApp.Application.Commands.Users;
using MvcTestApp.Application.Commands.Users.Create;
using MvcTestApp.Application.Commands.Users.Delete;
using MvcTestApp.Application.Commands.Users.Update;
using MvcTestApp.Application.Queries.Users;
using MvcTestApp.Authentication;
using MvcTestApp.Common.Serializers;
using MvcTestApp.Components;
using MvcTestApp.Domain.Users;
using MvcTestApp.Http;
using MvcTestApp.Infrastructure;
using MvcTestApp.Presenters.Users;

namespace MvcTestApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterApplication(this IServiceCollection services)
        {
            return services
                .AddScoped<ICreateUserUseCase, CreateUserUseCase>()
                .AddScoped<IUserFactory, UserFactory>()
                .AddScoped<IDeleteUserUseCase, DeleteUserUseCase>()
                .AddScoped<IUpdateUserUseCase, UpdateUserUseCase>()
                .AddScoped<IUserQueries, UserQueries>();
        }

        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
        {
            return services.AddSingleton<IUserRepository, UserRepository>();
        }

        public static IServiceCollection RegisterWeb(this IServiceCollection services)
        {
            return services
                .AddScoped<IAuthenticationService, AuthenticationService>()
                .AddScoped<ICreateUserPresenter, CreateUserPresenter>()
                .AddScoped<IDeleteUserPresenter, DeleteUserPresenter>()
                .AddScoped<IUpdateUserPresenter, UpdateUserPresenter>()
                .AddTransient<IContentTypeResolver, ContentTypeResolver>()
                .AddTransient<IApplicationContentType, ApplicationJsonContentType>()
                .AddTransient<IApplicationContentType, ApplicationXmlContentType>()
                .AddTransient<IExceptionToHttpStatusCodeParser, ExceptionToHttpStatusCodeParser>();
        }

        public static IServiceCollection RegisterCommon(this IServiceCollection services)
        {
            return services
                .AddTransient<IJsonSerializer, JsonSerializer>()
                .AddTransient<IXmlSerializer, XmlSerializer>();
        }
    }
}
