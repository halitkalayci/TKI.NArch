using Application.Features.Cars.Rules;
using Core.Application.Pipelines.Authorization;
using Core.CrossCuttingConcerns.Logging.Serilog.Logger;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
        });
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddTransient<CarBusinessRules>();

        services.AddTransient<LoggerServiceBase, FileLogger>();
        return services;
    }
}
