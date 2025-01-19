using Mapster;

using Zeus.Api.Presentation.Web.Contracts.Automations;
using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.Entities;

namespace Zeus.Api.Presentation.Web.Mapping;

public class AutomationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Automation, GetAutomationResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Label, src => src.Label)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.OwnerId, src => src.OwnerId.Value)
            .Map(dest => dest.Enabled, src => src.Enabled)
            .Map(dest => dest.UpdatedAt, src => src.UpdatedAt)
            .Map(dest => dest.Trigger, src => src.Trigger)
            .Map(dest => dest.Actions, src => src.Actions);

        config.NewConfig<AutomationTrigger, GetAutomationTriggerResponse>()
            .Map(dest => dest.Dependencies, src => Enumerable.Select(src.Dependencies, d => d.Value).ToArray());

        config.NewConfig<AutomationAction, GetAutomationActionResponse>()
            .Map(dest => dest.Dependencies, src => Enumerable.Select(src.Dependencies, d => d.Value).ToArray());
    }
}
