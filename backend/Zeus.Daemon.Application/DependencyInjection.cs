using System.Reflection;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using Zeus.BuildingBlocks.Application.Behaviors;
using Zeus.Daemon.Application.Attributes;
using Zeus.Daemon.Application.Discord.Services;
using Zeus.Daemon.Application.Extensions;
using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Application.Interfaces.HandlerProviders;
using Zeus.Daemon.Application.Interfaces.Registries;
using Zeus.Daemon.Application.Services;
using Zeus.Daemon.Application.Services.HandlerProviders;
using Zeus.Daemon.Application.Services.Registries;

namespace Zeus.Daemon.Application;

public static class DependencyInjection
{
    private static readonly Assembly Assembly = typeof(DependencyInjection).Assembly;

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTriggerHandlersFromAssembly();
        services.AddActionHandlersFromAssembly();
        services.AddSingleton<IActionHandlersProvider, ActionHandlersProvider>();
        services.AddSingleton<ITriggerHandlersProvider, TriggerHandlersProvider>();

        services.AddSingleton<ITriggersRegistry, TriggersRegistry>();
        services.AddSingleton<IAutomationsRegistry, AutomationsRegistry>();
        services.AddSingleton<IAutomationsRunner, AutomationsRunner>();
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

    private static IServiceCollection AddActionHandlersFromAssembly(this IServiceCollection services)
    {
        var types = Assembly.GetActionHandlersHostingTypes();

        foreach (var type in types)
        {
            services.AddTransient(type);
        }
        return services;
    }
}
