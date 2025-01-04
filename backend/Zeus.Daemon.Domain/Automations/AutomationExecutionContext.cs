using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;

namespace Zeus.Daemon.Domain.Automations;

public sealed class AutomationExecutionContext
{
    public Automation Automation { get; private set; }
    public IReadOnlyList<Integration> Integrations { get; private set; }

    public AutomationExecutionContext(
        Automation automation,
        List<Integration> integrations)
    {
        Automation = automation;
        Integrations = integrations.AsReadOnly();
    }
}
