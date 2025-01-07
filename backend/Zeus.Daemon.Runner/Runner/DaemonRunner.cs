using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Zeus.Daemon.Application.Discord.Services;
using Zeus.Daemon.Application.Interfaces.HandlerProviders;
using Zeus.Daemon.Application.Interfaces.Registries;
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

    public async Task Run(CancellationToken cancellationToken = default)
    {
        _serviceProvider.GetRequiredService<ITriggersRegistry>();
        _serviceProvider.GetRequiredService<IAutomationsRegistry>();
        _serviceProvider.GetRequiredService<ITriggerHandlersProvider>();
        _serviceProvider.GetRequiredService<IActionHandlersProvider>();

        await Task.WhenAll(
            ListenUpdatesAsync(cancellationToken)
        );
    }
}
