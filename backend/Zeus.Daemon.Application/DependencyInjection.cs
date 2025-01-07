using System.Reflection;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using Zeus.BuildingBlocks.Application.Behaviors;
using Zeus.Daemon.Application.Discord.Actions;
using Zeus.Daemon.Application.Discord.Services.Api;
using Zeus.Daemon.Application.Discord.Services.Websocket;
using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Application.Services;

namespace Zeus.Daemon.Application;

public static class DependencyInjection
{
    private static readonly Assembly Assembly = typeof(DependencyInjection).Assembly;

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, ServiceLifetime.Singleton);
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(ValidateBehavior<,>));

        services.AddSingleton<IDiscordWebSocketService, DiscordWebSocketService>();
        services.AddSingleton<IDiscordApiService, DiscordApiService>();

        services.AddTriggerHandlersFromAssembly();
        services.AddSingleton<ITriggersRegistry, TriggersRegistry>();
        return services;
    }

    private static IServiceCollection AddTriggerHandlersFromAssembly(this IServiceCollection services)
    {
        var types = Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(ITriggerHandler)) && !t.IsAbstract)
            .ToList();

        foreach (var type in types)
        {
            services.AddTransient(typeof(ITriggerHandler), type);
        }
        return services;
    }
}
