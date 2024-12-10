using Google.Protobuf.WellKnownTypes;

namespace Zeus.Api.gRPC.SDK;

public class SynchronizationGrpcService
{
    private readonly Synchronization.SynchronizationClient _client;

    public SynchronizationGrpcService(Synchronization.SynchronizationClient client)
    {
        _client = client;
    }

    public async Task<bool> HasChangesAsync(DateTime lastUpdate, CancellationToken cancellationToken = default)
    {
        var request = new SyncStateRequest { LastSyncTimestamp = Timestamp.FromDateTime(lastUpdate).Seconds };
        var response = await _client.GetSyncStateAsync(request, cancellationToken: cancellationToken);
        return response.HasChanges;
    }
}
