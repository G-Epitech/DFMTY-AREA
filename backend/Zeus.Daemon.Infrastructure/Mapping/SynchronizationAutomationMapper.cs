using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.Entities;
using Zeus.Common.Domain.AutomationAggregate.Enums;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Daemon.Infrastructure.Mapping;

public static class SynchronizationAutomationMapper
{
    public static Automation MapToAutomation(this Api.Presentation.gRPC.Contracts.Automation automation)
    {
        var automationId = new AutomationId(Guid.Parse(automation.Id));
        var ownerId = new UserId(Guid.Parse(automation.OwnerId));

        return new Automation(
            automationId,
            automation.Label,
            automation.Description,
            ownerId,
            MapToAutomationTrigger(automation.Trigger),
            automation.Actions.Select(MapToAutomationAction).ToList(),
            DateTimeOffset.FromUnixTimeSeconds(automation.UpdatedAt).DateTime,
            DateTimeOffset.FromUnixTimeSeconds(automation.CreatedAt).DateTime,
            automation.Enabled
        );
    }

    private static AutomationTrigger MapToAutomationTrigger(this Api.Presentation.gRPC.Contracts.AutomationTrigger trigger)
    {
        var automationTriggerId = new AutomationTriggerId(Guid.Parse(trigger.Id));

        return new AutomationTrigger(
            automationTriggerId,
            trigger.Identifier,
            trigger.Parameters.Select(p => new AutomationTriggerParameter { Identifier = p.Identifier, Value = p.Value }).ToList(),
            trigger.Providers.Select(p => new IntegrationId(Guid.Parse(p))).ToList()
        );
    }

    private static AutomationTriggerParameter MapToAutomationTriggerParameter(this Api.Presentation.gRPC.Contracts.AutomationTriggerParameter parameter)
    {
        return new AutomationTriggerParameter { Identifier = parameter.Identifier, Value = parameter.Value };
    }

    private static AutomationAction MapToAutomationAction(this Api.Presentation.gRPC.Contracts.AutomationAction action)
    {
        var automationActionId = new AutomationActionId(Guid.Parse(action.Id));

        return new AutomationAction(
            automationActionId,
            action.Identifier,
            action.Rank,
            action.Parameters.Select(p => new AutomationActionParameter
            {
                Identifier = p.Identifier, Value = p.Value, Type = Enum.Parse<AutomationActionParameterType>(p.Type)
            }).ToList(),
            action.Providers.Select(p => new IntegrationId(Guid.Parse(p))).ToList()
        );
    }
}
