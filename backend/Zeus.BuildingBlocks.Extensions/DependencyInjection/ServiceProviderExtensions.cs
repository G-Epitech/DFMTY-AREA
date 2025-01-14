using System.Reflection;

namespace Zeus.Common.Extensions.DependencyInjection;

public static class ServiceProviderExtensions
{
    public static IServiceProvider StartAutoServices(this IServiceProvider serviceProvider, Assembly assembly)
    {
        var types = assembly.GetAutoStartedServicesTypes();

        foreach (var type in types)
        {
            serviceProvider.GetService(type);
        }
        return serviceProvider;
    }

    public static IServiceProvider StartAutoServices(this IServiceProvider serviceProvider, Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            serviceProvider.StartAutoServices(assembly);
        }
        return serviceProvider;
    }

    public static IServiceProvider StartAutoServices(this IServiceProvider serviceProvider)
    {
        return serviceProvider.StartAutoServices(AppDomain.CurrentDomain.GetAssemblies());
    }
}
