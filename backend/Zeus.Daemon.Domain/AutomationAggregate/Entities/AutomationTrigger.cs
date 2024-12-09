using Zeus.Common.Domain.Models;
using Zeus.Daemon.Domain.AutomationAggregate.ValueObjects;
using Zeus.Daemon.Domain.IntegrationAggregate.ValueObjects;

namespace Zeus.Daemon.Domain.AutomationAggregate.Entities;

public sealed class AutomationTrigger : Entity<AutomationTriggerId>
{
    private readonly List<DynamicParameter> _parameters;
    private readonly List<IntegrationId> _providers;

    public string Identifier { get; private set; }
    public IReadOnlyList<DynamicParameter> Parameters => _parameters.AsReadOnly();
    public IReadOnlyList<IntegrationId> Providers => _providers.AsReadOnly();

    private AutomationTrigger(
        AutomationTriggerId id,
        string identifier,
        List<DynamicParameter> parameters,
        List<IntegrationId> providers)
        : base(id)
    {
        Identifier = identifier;
        _parameters = parameters;
        _providers = providers;
    }

    public static AutomationTrigger Create(string identifier, List<DynamicParameter> parameters, List<IntegrationId> providers)
    {
        return new AutomationTrigger(
            AutomationTriggerId.CreateUnique(),
            identifier,
            parameters,
            providers);
    }

#pragma warning disable CS8618
    private AutomationTrigger()
    {
    }
#pragma warning restore CS8618
}
