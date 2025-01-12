using MassTransit;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Zeus.Common.Extensions.DependencyInjection;
using Zeus.Common.Extensions.Environment;
using Zeus.Daemon.Application.Discord.Services;
using Zeus.Daemon.Infrastructure.Automations;

namespace Zeus.Daemon.Runner.Runner;

public class DaemonRunner
{
    private readonly IConfigurationRoot _configuration;
    private readonly IEnvironmentProvider _environmentProvider;
    private readonly ILogger<DaemonRunner> _logger;
    private readonly IServiceProvider _serviceProvider;

    public DaemonRunner(IServiceProvider serviceProvider, IConfigurationRoot configuration)
    {
        _serviceProvider = serviceProvider;
        _configuration = configuration;
        _logger = serviceProvider.GetRequiredService<ILogger<DaemonRunner>>();
        _environmentProvider = serviceProvider.GetRequiredService<IEnvironmentProvider>();
        
        _logger.LogInformation("Daemon runner initialized");
        _serviceProvider.StartAutoServices();
        _logger.LogInformation("Daemon runner auto-started services started");
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
        _logger.LogInformation("DaemonRunner running. Environment is {environment}.", _environmentProvider.EnvironmentName);
        await Task.WhenAll(
            ListenDiscordAsync(cancellationToken)
        );
    }
}
