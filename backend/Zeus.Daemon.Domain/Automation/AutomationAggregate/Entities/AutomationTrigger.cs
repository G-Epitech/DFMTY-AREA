using Zeus.Common.Domain.Models;
using Zeus.Daemon.Domain.Automation.AutomationAggregate.ValueObjects;
using Zeus.Daemon.Domain.IntegrationAggregate.ValueObjects;

namespace Zeus.Daemon.Domain.Automation.AutomationAggregate.Entities;

public sealed class AutomationTrigger : Entity<AutomationTriggerId>
{
    private readonly List<AutomationTriggerParameter> _parameters;
    private readonly List<IntegrationId> _providers;

    public string Identifier { get; private set; }
    public IReadOnlyList<AutomationTriggerParameter> Parameters => _parameters.AsReadOnly();
    public IReadOnlyList<IntegrationId> Providers => _providers.AsReadOnly();

    public AutomationTrigger(
        AutomationTriggerId id,
        string identifier,
        List<AutomationTriggerParameter> parameters,
        List<IntegrationId> providers)
        : base(id)
    {
        Identifier = identifier;
        _parameters = parameters;
        _providers = providers;
    }

    public static AutomationTrigger Create(string identifier, List<AutomationTriggerParameter> parameters,
        List<IntegrationId> providers)
    {
        return new AutomationTrigger(
            AutomationTriggerId.CreateUnique(),
            identifier,
            parameters,
            providers);
    }
}
