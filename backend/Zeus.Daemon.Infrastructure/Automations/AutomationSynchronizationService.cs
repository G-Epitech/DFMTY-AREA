using Zeus.Api.Presentation.gRPC.SDK.Services;
using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Application.Interfaces.Registries;
using Zeus.Daemon.Infrastructure.Mapping;

namespace Zeus.Daemon.Infrastructure.Automations;

public class AutomationSynchronizationService
{
    private readonly SynchronizationGrpcService _synchronizationGrpcService;
    private readonly IAutomationsRegistry _automationsRegistry;
    private DateTime _lastUpdate = DateTime.UnixEpoch;
    private const int RefreshIntervalMilliseconds = 1000;

    public AutomationSynchronizationService(SynchronizationGrpcService synchronizationGrpcService,
        IAutomationsRegistry automationsRegistry)
    {
        _synchronizationGrpcService = synchronizationGrpcService;
        _automationsRegistry = automationsRegistry;
    }

    public async Task ListenUpdatesAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await WaitForChangesAsync(cancellationToken);
            Console.WriteLine("New automations to pull");
            await RefreshAutomationsAsync(cancellationToken);
        }
    }

    private async Task WaitForChangesAsync(CancellationToken cancellationToken = default)
    {
        bool hasChanges = false;

        while (!cancellationToken.IsCancellationRequested && !hasChanges)
        {
            hasChanges = await _synchronizationGrpcService.HasChangesAsync(_lastUpdate, cancellationToken);
            await Task.Delay(RefreshIntervalMilliseconds, cancellationToken);
        }
    }

    private async Task RefreshAutomationsAsync(CancellationToken cancellationToken = default)
    {
        var delta = await _synchronizationGrpcService.SyncDeltaAsync(_lastUpdate, cancellationToken);

        _lastUpdate = DateTime.UtcNow;

        var automations = delta.Select(d => d.MapToAutomation()).ToList();

        Console.WriteLine($"Syncing {automations.Count} automations");
        Task.WaitAll(
            automations.Select(a => _automationsRegistry.RegisterAsync(a, cancellationToken)).ToList(), cancellationToken);
    }
}
