using Microsoft.Extensions.Logging;

using Zeus.Api.Presentation.gRPC.SDK.Services;
using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Application.Interfaces.Registries;
using Zeus.Daemon.Infrastructure.Services.Api.Mapping;

namespace Zeus.Daemon.Infrastructure.Services.Api;

public class SynchronizationService : IDaemonService
{
    private const int MaxRetries = 3;
    private const int RetryDelayMilliseconds = 5000;
    private readonly IAutomationsService _automationService;
    private readonly IAutomationsRegistry _automationsRegistry;
    private readonly ILogger _logger;
    private CancellationTokenSource? _currentSyncingToken;

    public SynchronizationService(IAutomationsService automationService,
        IAutomationsRegistry automationsRegistry, ILogger<SynchronizationService> logger)
    {
        _automationService = automationService;
        _automationsRegistry = automationsRegistry;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return SyncAutomationsAsync();
    }

    public Task StopAsync()
    {
        return Task.CompletedTask;
    }

    public async Task CancelPendingSyncAsync()
    {
        if (_currentSyncingToken is not null)
        {
            await _currentSyncingToken.CancelAsync();
        }
        _currentSyncingToken = null;
    }

    private async Task SyncAutomationsAsync()
    {
        var retryCount = 0;

        if (_currentSyncingToken is not null)
        {
            await _currentSyncingToken.CancelAsync();
        }

        _currentSyncingToken = new CancellationTokenSource();

        while (retryCount++ < MaxRetries && !_currentSyncingToken.IsCancellationRequested)
        {
            try
            {
                var automations = _automationService.GetRegistrableAutomationsAsync();
                var enumerator = automations.GetAsyncEnumerator();
                var loadedAutomationsCount = 0;

                while (await enumerator.MoveNextAsync())
                {
                    try
                    {
                        if (await _automationsRegistry.RegisterAsync(enumerator.Current.MapToRegistrableAutomation()))
                        {
                            loadedAutomationsCount++;
                        }
                        else
                        {
                            _logger.LogWarning("Automation {id} has not correctly been registered", enumerator.Current.Automation.Id);
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, "Failed to register automation {id}", enumerator.Current.Automation.Id);
                    }
                }
                if (loadedAutomationsCount > 0)
                {
                    _logger.LogInformation("Loaded {count} automations", loadedAutomationsCount);
                }
                break;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to sync automations");
            }
            if (retryCount >= MaxRetries)
            {
                _logger.LogCritical("Max retries reached, aborting");
                break;
            }
            _logger.LogInformation("Retrying in {delay}ms", RetryDelayMilliseconds);
            await Task.Delay(RetryDelayMilliseconds, _currentSyncingToken.Token);
        }
        _currentSyncingToken = null;
    }
}
