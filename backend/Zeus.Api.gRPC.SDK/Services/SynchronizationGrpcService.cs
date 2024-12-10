namespace Zeus.Api.gRPC.SDK.Services;

public class SynchronizationGrpcService
{
    private readonly Synchronization.SynchronizationClient _client;

    public SynchronizationGrpcService(Synchronization.SynchronizationClient client)
    {
        _client = client;
    }

    public async Task<bool> HasChangesAsync(DateTime lastUpdate, CancellationToken cancellationToken = default)
    {
        var timestamp = new DateTimeOffset(lastUpdate.ToUniversalTime()).ToUnixTimeSeconds();

        var request = new SyncStateRequest { LastSyncTimestamp = timestamp };
        var response = await _client.GetSyncStateAsync(request, cancellationToken: cancellationToken);

        return response.HasChanges;
    }

    public async Task<IList<Automation>> SyncDeltaAsync(DateTime lastUpdate, CancellationToken cancellationToken = default)
    {
        var timestamp = new DateTimeOffset(lastUpdate.ToUniversalTime()).ToUnixTimeSeconds();

        var request = new SyncDeltaRequest { LastSyncTimestamp = timestamp };
        var response = await _client.SyncDeltaAsync(request, cancellationToken: cancellationToken);

        return response.Automations;
    }
}
