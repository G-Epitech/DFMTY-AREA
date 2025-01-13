using Microsoft.Extensions.DependencyInjection;

namespace Zeus.Common.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    private static void AddWithLifetime<TService>(this IServiceCollection services, ServiceLifetime lifetime)
        where TService : class
    {
        switch (lifetime)
        {
            case ServiceLifetime.Singleton:
                services.AddSingleton<TService>();
                break;
            case ServiceLifetime.Scoped:
                services.AddScoped<TService>();
                break;
            case ServiceLifetime.Transient:
                services.AddTransient<TService>();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
        }
    }

    public static IServiceCollection AddService<TImplementation, TService1>(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient)
        where TImplementation : class, TService1
    {
        services.AddWithLifetime<TImplementation>(lifetime);
        services.Add(new ServiceDescriptor(typeof(TService1), x => x.GetRequiredService<TImplementation>(), lifetime));
        return services;
    }

    public static IServiceCollection AddService<TImplementation, TService1, TService2>(this IServiceCollection services, ServiceLifetime lifetime
        = ServiceLifetime.Transient)
        where TImplementation : class, TService1, TService2
    {
        services.AddWithLifetime<TImplementation>(lifetime);
        services.Add(new ServiceDescriptor(typeof(TService1), x => x.GetRequiredService<TImplementation>(), lifetime));
        services.Add(new ServiceDescriptor(typeof(TService2), x => x.GetRequiredService<TImplementation>(), lifetime));
        return services;
    }

    public static IServiceCollection AddService<TImplementation, TService1, TService2, TService3>(this IServiceCollection services, ServiceLifetime lifetime
        = ServiceLifetime.Transient)
        where TImplementation : class, TService1, TService2, TService3
    {
        services.AddWithLifetime<TImplementation>(lifetime);
        services.Add(new ServiceDescriptor(typeof(TService1), x => x.GetRequiredService<TImplementation>(), lifetime));
        services.Add(new ServiceDescriptor(typeof(TService2), x => x.GetRequiredService<TImplementation>(), lifetime));
        services.Add(new ServiceDescriptor(typeof(TService3), x => x.GetRequiredService<TImplementation>(), lifetime));
        return services;
    }

    public static IServiceCollection AddService<TImplementation, TService1, TService2, TService3, TService4>(this IServiceCollection services,
        ServiceLifetime lifetime
            = ServiceLifetime.Transient)
        where TImplementation : class, TService1, TService2, TService3, TService4
    {
        services.AddWithLifetime<TImplementation>(lifetime);
        services.Add(new ServiceDescriptor(typeof(TService1), x => x.GetRequiredService<TImplementation>(), lifetime));
        services.Add(new ServiceDescriptor(typeof(TService2), x => x.GetRequiredService<TImplementation>(), lifetime));
        services.Add(new ServiceDescriptor(typeof(TService3), x => x.GetRequiredService<TImplementation>(), lifetime));
        services.Add(new ServiceDescriptor(typeof(TService4), x => x.GetRequiredService<TImplementation>(), lifetime));
        return services;
    }

    public static IServiceCollection AddService<TImplementation, TService1, TService2, TService3, TService4, TService5>(
        this IServiceCollection services, ServiceLifetime lifetime
            = ServiceLifetime.Transient)
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5
    {
        services.AddWithLifetime<TImplementation>(lifetime);
        services.Add(new ServiceDescriptor(typeof(TService1), x => x.GetRequiredService<TImplementation>(), lifetime));
        services.Add(new ServiceDescriptor(typeof(TService2), x => x.GetRequiredService<TImplementation>(), lifetime));
        services.Add(new ServiceDescriptor(typeof(TService3), x => x.GetRequiredService<TImplementation>(), lifetime));
        services.Add(new ServiceDescriptor(typeof(TService4), x => x.GetRequiredService<TImplementation>(), lifetime));
        services.Add(new ServiceDescriptor(typeof(TService5), x => x.GetRequiredService<TImplementation>(), lifetime));
        return services;
    }

    public static IServiceCollection AddService<TImplementation, TService1, TService2, TService3, TService4, TService5, TService6>(
        this IServiceCollection services, ServiceLifetime lifetime
            = ServiceLifetime.Transient)
        where TImplementation : class, TService1, TService2, TService3, TService4, TService5, TService6
    {
        services.AddWithLifetime<TImplementation>(lifetime);
        services.Add(new ServiceDescriptor(typeof(TService1), x => x.GetRequiredService<TImplementation>(), lifetime));
        services.Add(new ServiceDescriptor(typeof(TService2), x => x.GetRequiredService<TImplementation>(), lifetime));
        services.Add(new ServiceDescriptor(typeof(TService3), x => x.GetRequiredService<TImplementation>(), lifetime));
        services.Add(new ServiceDescriptor(typeof(TService4), x => x.GetRequiredService<TImplementation>(), lifetime));
        services.Add(new ServiceDescriptor(typeof(TService5), x => x.GetRequiredService<TImplementation>(), lifetime));
        services.Add(new ServiceDescriptor(typeof(TService6), x => x.GetRequiredService<TImplementation>(), lifetime));
        return services;
    }
}
