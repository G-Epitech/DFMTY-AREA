using Microsoft.Extensions.DependencyInjection;

using Zeus.Daemon.Application.Discord.Services.Websocket;
using Zeus.Daemon.Infrastructure.Automations;

namespace Zeus.Daemon.Runner.Runner;

public class DaemonRunner
{
    private readonly IServiceProvider _serviceProvider;

    public DaemonRunner(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
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

    public async Task Run(CancellationToken cancellationToken = default)
    {
        await Task.WhenAll(
            ListenUpdatesAsync(cancellationToken),
            ListenDiscordAsync(cancellationToken)
        );
    }
}
