using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Zeus.Daemon.Application.Discord.Services.Websocket;
using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Infrastructure.Automations;

namespace Zeus.Daemon.Runner.Runner;

public class DaemonRunner
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfigurationRoot _configuration;

    public DaemonRunner(IServiceProvider serviceProvider, IConfigurationRoot configuration)
    {
        _serviceProvider = serviceProvider;
        _configuration = configuration;
    }

    private Task ListenUpdatesAsync(CancellationToken cancellationToken = default)
    {
        var automationsListener = _serviceProvider.GetRequiredService<AutomationSynchronizationService>();
        return automationsListener.ListenUpdatesAsync(cancellationToken);
    }

    private Task ListenDiscordAsync(CancellationToken cancellationToken = default)
    {
        var discord = _serviceProvider.GetRequiredService<IDiscordWebSocketService>();

        return discord.ConnectAsync(cancellationToken);
    }

    public Task Run(CancellationToken cancellationToken = default)
    {
        var svc = _serviceProvider.GetService<ITriggersRegistry>();
        while (true) ;
        /*await Task.WhenAll(
            ListenUpdatesAsync(cancellationToken),
            ListenDiscordAsync(cancellationToken)
        );*/
    }
}
