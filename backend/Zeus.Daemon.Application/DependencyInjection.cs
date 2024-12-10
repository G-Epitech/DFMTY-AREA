using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using Zeus.Common.Application.Behaviors;
using Zeus.Daemon.Application.Interfaces.Services.Websockets;
using Zeus.Daemon.Application.Services.Websocket;

namespace Zeus.Daemon.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, ServiceLifetime.Singleton);
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(ValidateBehavior<,>));

        services.AddSingleton<IDiscordWebSocketService, DiscordWebSocketService>();
        return services;
    }
}
