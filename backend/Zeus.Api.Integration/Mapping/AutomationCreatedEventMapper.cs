using Mapster;

using Zeus.Api.Integration.Contracts;
using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.Entities;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Integration.Mapping;

public class AutomationCreatedEventMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Automation, AutomationCreatedEvent>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.OwnerId, src => src.OwnerId.Value)
            .Map(dest => dest.Trigger, src => src.Trigger)
            .Map(dest => dest.Actions, src => src.Actions);

        config.NewConfig<AutomationAction, AutomationCreatedEventAction>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Parameters, src => src.Parameters)
            .Map(dest => dest.Providers, src => src.Dependencies);

        config.NewConfig<IntegrationId, Guid>()
            .Map(dest => dest, src => src.Value);

        config.NewConfig<AutomationTrigger, AutomationCreatedEventTrigger>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Parameters, src => src.Parameters)
            .Map(dest => dest.Providers, src => src.Dependencies);

        config.NewConfig<AutomationCreatedEvent, Automation>()
            .MapWith(raw => new Automation(
                new AutomationId(raw.Id),
                raw.Label,
                raw.Description,
                new UserId(raw.OwnerId),
                new AutomationTrigger(
                    new AutomationTriggerId(raw.Trigger.Id),
                    raw.Trigger.Identifier,
                    raw.Trigger.Parameters.Select(p => new AutomationTriggerParameter { Value = p.Value, Identifier = p.Identifier }).ToList(),
                    raw.Trigger.Providers.Select(p => new IntegrationId(p)).ToList()
                ),
                raw.Actions.Select(a => new AutomationAction(
                    new AutomationActionId(a.Id),
                    a.Identifier,
                    a.Rank,
                    a.Parameters.Select(p => new AutomationActionParameter { Value = p.Value, Identifier = p.Identifier, Type = p.Type }).ToList(),
                    a.Providers.Select(p => new IntegrationId(p)).ToList()
                )).ToList(),
                raw.CreatedAt,
                raw.UpdatedAt,
                raw.Enabled)
            );
    }
}
