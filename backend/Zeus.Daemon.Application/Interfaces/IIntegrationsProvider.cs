using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;

namespace Zeus.Daemon.Application.Interfaces;

public interface IIntegrationsProvider
{
    public Task<Dictionary<IntegrationId, Integration>> GetActionsIntegrationsByAutomationIdsAsync(IReadOnlyList<AutomationId> automationIds,
        CancellationToken cancellationToken = default);

    public Task<Dictionary<IntegrationId, Integration>> GetTriggerIntegrationsByAutomationIdAsync(AutomationId automationId,
        CancellationToken cancellationToken = default);
}
