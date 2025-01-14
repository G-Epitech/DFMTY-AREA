using Zeus.Api.Presentation.gRPC.Contracts;
using Zeus.Api.Presentation.gRPC.SDK.Services;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Infrastructure.Mapping;

using Integration = Zeus.Common.Domain.Integrations.IntegrationAggregate.Integration;

namespace Zeus.Daemon.Infrastructure.Services.Api;

public class IntegrationsProvider : IIntegrationsProvider
{
    private readonly IIntegrationsService _integrationService;

    public IntegrationsProvider(IIntegrationsService integrationService)
    {
        _integrationService = integrationService;
    }

    public async Task<Dictionary<IntegrationId, Integration>> GetActionsIntegrationsByAutomationIdsAsync(IReadOnlyList<AutomationId> automationIds,
        CancellationToken cancellationToken = default)
    {
        var integrations =
            await _integrationService.GetAutomationsIntegrationsAsync(automationIds.Select(x => x.Value).ToList(), IntegrationSource.Action, cancellationToken);

        return integrations.ToDictionary(x => IntegrationId.Parse(x.Id), x => x.MapToIntegration());
    }

    public async Task<Dictionary<IntegrationId, Integration>> GetTriggerIntegrationsByAutomationIdAsync(AutomationId automationId,
        CancellationToken cancellationToken = default)
    {
        var integrations = await _integrationService.GetAutomationsIntegrationsAsync([automationId.Value], IntegrationSource.Trigger, cancellationToken);

        return integrations.ToDictionary(x => IntegrationId.Parse(x.Id), x => x.MapToIntegration());
    }
}
