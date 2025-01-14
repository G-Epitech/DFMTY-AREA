using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;

namespace Zeus.Daemon.Domain.Automations;

public record RegistrableAutomation
{
    public required Automation Automation { get; init; }
    public required IReadOnlyList<Integration> TriggerIntegrations { get; init; }
}
