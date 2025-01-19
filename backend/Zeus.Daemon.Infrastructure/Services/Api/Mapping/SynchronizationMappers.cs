using Zeus.Common.Domain.AutomationAggregate.Enums;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

using Automation = Zeus.Common.Domain.AutomationAggregate.Automation;
using AutomationAction = Zeus.Common.Domain.AutomationAggregate.Entities.AutomationAction;
using AutomationActionParameter = Zeus.Common.Domain.AutomationAggregate.ValueObjects.AutomationActionParameter;
using AutomationTrigger = Zeus.Common.Domain.AutomationAggregate.Entities.AutomationTrigger;
using AutomationTriggerParameter = Zeus.Common.Domain.AutomationAggregate.ValueObjects.AutomationTriggerParameter;
using Integration = Zeus.Common.Domain.Integrations.IntegrationAggregate.Integration;
using IntegrationToken = Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects.IntegrationToken;
using IntegrationTokenUsage = Zeus.Common.Domain.Integrations.IntegrationAggregate.Enums.IntegrationTokenUsage;
using IntegrationType = Zeus.Common.Domain.Integrations.Common.Enums.IntegrationType;
using RegistrableAutomation = Zeus.Daemon.Domain.Automations.RegistrableAutomation;

namespace Zeus.Daemon.Infrastructure.Services.Api.Mapping;

using RegistrableAutomation = RegistrableAutomation;

public static class SynchronizationMappers
{
    public static RegistrableAutomation MapToRegistrableAutomation(this Zeus.Api.Presentation.gRPC.Contracts.RegistrableAutomation registrable)
    {
        var automationId = new AutomationId(Guid.Parse(registrable.Automation.Id));
        var ownerId = new UserId(Guid.Parse(registrable.Automation.OwnerId));

        return new RegistrableAutomation
        {
            Automation = new Automation(
                automationId,
                registrable.Automation.Label,
                registrable.Automation.Description,
                registrable.Automation.Color,
                registrable.Automation.Icon,
                ownerId,
                MapToAutomationTrigger(registrable.Automation.Trigger),
                registrable.Automation.Actions.Select(MapToAutomationAction).ToList(),
                DateTimeOffset.FromUnixTimeSeconds(registrable.Automation.UpdatedAt).DateTime,
                DateTimeOffset.FromUnixTimeSeconds(registrable.Automation.CreatedAt).DateTime,
                registrable.Automation.Enabled
            ),
            TriggerIntegrations = registrable.TriggerDependencies.Select(i => i.MapToIntegration()).ToList()
        };
    }

    private static AutomationTrigger MapToAutomationTrigger(this Zeus.Api.Presentation.gRPC.Contracts.AutomationTrigger trigger)
    {
        var automationTriggerId = new AutomationTriggerId(Guid.Parse(trigger.Id));

        return new AutomationTrigger(
            automationTriggerId,
            trigger.Identifier,
            trigger.Parameters.Select(MapToAutomationTriggerParameter).ToList(),
            trigger.Dependencies.Select(p => new IntegrationId(Guid.Parse(p))).ToList()
        );
    }

    private static AutomationTriggerParameter MapToAutomationTriggerParameter(this Zeus.Api.Presentation.gRPC.Contracts.AutomationTriggerParameter parameter)
    {
        return new AutomationTriggerParameter { Identifier = parameter.Identifier, Value = parameter.Value };
    }

    private static AutomationAction MapToAutomationAction(this Zeus.Api.Presentation.gRPC.Contracts.AutomationAction action)
    {
        var automationActionId = new AutomationActionId(Guid.Parse(action.Id));

        return new AutomationAction(
            automationActionId,
            action.Identifier,
            action.Rank,
            action.Parameters.Select(p => new AutomationActionParameter
            {
                Identifier = p.Identifier,
                Value = p.Value,
                Type = Enum.Parse<AutomationActionParameterType>(p.Type)
            }).ToList(),
            action.Dependencies.Select(p => new IntegrationId(Guid.Parse(p))).ToList()
        );
    }

    public static Integration MapToIntegration(this Zeus.Api.Presentation.gRPC.Contracts.Integration integration)
    {
        var id = new IntegrationId(Guid.Parse(integration.Id));
        var ownerId = new UserId(Guid.Parse(integration.OwnerId));
        var type = Enum.Parse<IntegrationType>(integration.Type.ToString());
        var clientId = integration.ClientId;
        var updatedAt = DateTimeOffset.FromUnixTimeSeconds(integration.UpdatedAt).DateTime;
        var createdAt = DateTimeOffset.FromUnixTimeSeconds(integration.CreatedAt).DateTime;
        var tokens = integration.Tokens.Select(t => new IntegrationToken(t.Value, t.Type, Enum.Parse<IntegrationTokenUsage>(t.Usage.ToString()))).ToList();

        return type switch
        {
            IntegrationType.Discord => new DiscordIntegration(id, ownerId, clientId, tokens, updatedAt, createdAt),
            IntegrationType.Gmail => new GmailIntegration(id, ownerId, clientId, tokens, updatedAt, createdAt),
            IntegrationType.Notion => new NotionIntegration(id, ownerId, clientId, tokens, updatedAt, createdAt),
            IntegrationType.OpenAi => new OpenAiIntegration(id, ownerId, clientId, tokens, updatedAt, createdAt),
            IntegrationType.LeagueOfLegends => new LeagueOfLegendsIntegration(id, ownerId, clientId, tokens, updatedAt, createdAt),
            IntegrationType.Github => new GithubIntegration(id, ownerId, clientId, tokens, updatedAt, createdAt),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}
