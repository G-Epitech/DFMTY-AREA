using Zeus.Daemon.Domain.IntegrationAggregate;

namespace Zeus.Daemon.Domain.Automation;

public sealed class AutomationExecutionContext
{
    public AutomationAggregate.Automation Automation { get; private set; }
    public IReadOnlyList<Integration> Integrations { get; private set; }

    public AutomationExecutionContext(
        AutomationAggregate.Automation automation,
        List<Integration> integrations)
    {
        Automation = automation;
        Integrations = integrations.AsReadOnly();
    }
}
