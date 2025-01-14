using System.Reflection;

using MassTransit;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Zeus.Api.Infrastructure.Settings.MessageBrokers;

namespace Zeus.Api.Infrastructure.Integrations;

public static class DependencyInjection
{
    public static IServiceCollection AddMessageBroker(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly? assembly
    )
    {
        var rabbitMqSettings = new RabbitMqSettings();
        configuration.GetSection(RabbitMqSettings.SectionName).Bind(rabbitMqSettings);

        services.AddMassTransit(busConfiguration =>
        {
            if (assembly is not null)
            {
                busConfiguration.AddConsumers(assembly);
            }

            busConfiguration.UsingRabbitMq((ctx, rabbitConfiguration) =>
            {
                rabbitConfiguration.ConfigureJsonSerializerOptions(c =>
                {
                    c.IncludeFields = true;
                    return c;
                });

                rabbitConfiguration.Host(new Uri(rabbitMqSettings.Host), host =>
                {
                    host.Username(rabbitMqSettings.Username);
                    host.Password(rabbitMqSettings.Password);
                });
                rabbitConfiguration.ConfigureEndpoints(ctx);
            });
        });
        return services;
    }
}
