﻿using System.Reflection;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using MyApp.Application.Common.Behaviours;
using MyApp.Application.Common.Interfaces;

namespace MyApp.Application;

public static class ConfigureServices
{

    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {

        services.AddAutoMapper(Assembly.GetExecutingAssembly());


        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });


        // NOTE: Do not register the Custom LoggingBehaviour, as this would cause a circular dependency for the service.
        //       It is automatically registered by the default ILogger
        // services.AddSingleton(typeof(Microsoft.Extensions.Logging.ILogger<>), typeof(LoggingBehaviour<>));

       
        return services;
    
    }

}
