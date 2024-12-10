using Google.Protobuf.WellKnownTypes;

using Grpc.Core;

using MediatR;

using Zeus.Api.Application.Synchronization.Queries;
using Zeus.Api.Domain.AutomationAggregate.Enums;

namespace Zeus.Api.gRPC.Services;

public class SynchronizationService : Synchronization.SynchronizationBase
{
    private readonly ILogger<SynchronizationService> _logger;
    private readonly ISender _sender;

    public SynchronizationService(ILogger<SynchronizationService> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    public override async Task<SyncStateResponse> GetSyncState(SyncStateRequest request, ServerCallContext context)
    {
        var lastUpdate = await _sender.Send(new GetAutomationsLastUpdateQuery(AutomationState.Any)) ?? DateTime.MinValue;
        var lastUpdateTimestamp = new DateTimeOffset(lastUpdate.ToUniversalTime()).ToUnixTimeSeconds();
        
        return new SyncStateResponse
        {
            HasChanges = lastUpdateTimestamp > request.LastSyncTimestamp
        };
    }
}
