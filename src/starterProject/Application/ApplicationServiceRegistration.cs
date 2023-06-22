using Application.Services.Auth;
using Application.Services.Authenticator;
using Application.Services.Models;
using Application.Services.UserService;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Validation;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.CrossCuttingConcerns.Logging.Serilog.Logger;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
            config.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
        });
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddTransient<LoggerServiceBase, FileLogger>();
        services.AddScoped<IModelsService, ModelsManager>();
        services.AddScoped<IAuthService, AuthManager>();
        services.AddScoped<IUserService, UserManager>();
        services.AddScoped<IAuthenticatorService,AuthenticatorManager>();

        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));
        return services;
    }

    public static IServiceCollection AddSubClassesOfType(this IServiceCollection services, Assembly assembly, Type type)
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();

        foreach (var item in types)
        {
            services.AddScoped(item);
        }

        return services;
    }
}
