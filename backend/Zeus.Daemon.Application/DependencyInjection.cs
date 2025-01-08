using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

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
        services.AddSingleton<IAutomationsLauncher, AutomationLauncher>();
        return services;
    }

    private static IServiceCollection AddTriggerHandlersFromAssembly(this IServiceCollection services)
    {
        var types = Assembly.GetTriggerHandlersTypes();

        foreach (var type in types)
        {
            services.AddSingleton(type);
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
