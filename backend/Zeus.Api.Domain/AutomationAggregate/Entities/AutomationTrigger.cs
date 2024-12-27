using Zeus.Api.Domain.AutomationAggregate.Enums;
using Zeus.Api.Domain.AutomationAggregate.ValueObjects;
using Zeus.Api.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.AutomationAggregate.Entities;

public sealed class AutomationTrigger : Entity<AutomationTriggerId>
{
    private readonly List<AutomationTriggerParameter> _parameters;
    private readonly List<IntegrationId> _providers;

    public string Identifier { get; private set; }
    public IReadOnlyList<AutomationTriggerParameter> Parameters => _parameters.AsReadOnly();
    public IReadOnlyList<IntegrationId> Providers => _providers.AsReadOnly();

    private AutomationTrigger(
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

    public static AutomationTrigger Create(string identifier, List<AutomationTriggerParameter> parameters, List<IntegrationId> providers)
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
