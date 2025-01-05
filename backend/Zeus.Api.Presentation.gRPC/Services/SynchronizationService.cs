using Grpc.Core;

using MediatR;

using Zeus.Api.Application.Synchronization.Queries.GetAutomationsLastUpdate;
using Zeus.Api.Application.Synchronization.Queries.GetAutomationsUpdateAfter;
using Zeus.Api.Presentation.gRPC.Contracts;
using Zeus.Common.Domain.AutomationAggregate.Enums;

namespace Zeus.Api.Presentation.gRPC.Services;

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

        return new SyncStateResponse { HasChanges = lastUpdateTimestamp > request.LastSyncTimestamp };
    }

    public override async Task<SyncDeltaResponse> SyncDelta(SyncDeltaRequest request, ServerCallContext context)
    {
        var limitDate = DateTimeOffset.FromUnixTimeSeconds(request.LastSyncTimestamp).DateTime;
        var automations = await _sender.Send(new GetAutomationsUpdateAfterQuery(AutomationState.Any, limitDate));

        return new SyncDeltaResponse()
        {
            Automations =
            {
                automations.Select(a => new Automation
                {
                    Id = a.Id.Value.ToString(),
                    Label = a.Label,
                    Description = a.Description,
                    CreatedAt = new DateTimeOffset(a.CreatedAt.ToUniversalTime()).ToUnixTimeSeconds(),
                    UpdatedAt = new DateTimeOffset(a.UpdatedAt.ToUniversalTime()).ToUnixTimeSeconds(),
                    Enabled = a.Enabled,
                    OwnerId = a.OwnerId.Value.ToString(),
                    Actions =
                    {
                        a.Actions.Select(action => new AutomationAction
                        {
                            Id = action.Id.Value.ToString(),
                            Identifier = action.Identifier,
                            Parameters =
                            {
                                action.Parameters.Select(p =>
                                    new AutomationActionParameter { Identifier = p.Identifier, Type = p.Type.ToString(), Value = p.Value })
                            },
                            Providers = { action.Providers.Select(p => p.Value.ToString()) },
                            Rank = action.Rank,
                        })
                    },
                    Trigger = new AutomationTrigger()
                    {
                        Id = a.Trigger.Id.Value.ToString(),
                        Identifier = a.Trigger.Identifier,
                        Parameters = { a.Trigger.Parameters.Select(p => new AutomationTriggerParameter { Identifier = p.Identifier, Value = p.Value }) },
                        Providers = { a.Trigger.Providers.Select(p => p.Value.ToString()) }
                    }
                })
            }
        };
    }
}
