using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Zeus.Common.Extensions.DependencyInjection;
using Zeus.Common.Extensions.Environment;
using Zeus.Daemon.Application.Interfaces;

namespace Zeus.Daemon.Runner.Runner;

public class DaemonRunner
{
    private const int ThreadSleepTimeMilliseconds = 1000;
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

    private IReadOnlyList<IDaemonService> DaemonServices => _serviceProvider.GetServices<IDaemonService>().ToList();

    private Task StartDaemonServicesAsync(CancellationToken cancellationToken = default)
    {
        return Task.WhenAll(DaemonServices.Select(s => s.StartAsync(cancellationToken)));
    }

    private Task StopDaemonServicesAsync()
    {
        return Task.WhenAll(DaemonServices.Select(s => s.StopAsync()));
    }

    public async Task Run(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("DaemonRunner running. Environment is {environment}.", _environmentProvider.EnvironmentName);
        await StartDaemonServicesAsync(cancellationToken);

        while (!cancellationToken.IsCancellationRequested)
        {
            Thread.Sleep(ThreadSleepTimeMilliseconds);
        }
        _logger.LogInformation("DaemonRunner stopping.");
        await StopDaemonServicesAsync();
        _logger.LogInformation("DaemonRunner stopped.");
    }
}
