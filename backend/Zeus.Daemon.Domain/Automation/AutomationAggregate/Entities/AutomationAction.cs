using Zeus.Common.Domain.Models;
using Zeus.Daemon.Domain.Automation.AutomationAggregate.ValueObjects;
using Zeus.Daemon.Domain.IntegrationAggregate.ValueObjects;

namespace Zeus.Daemon.Domain.Automation.AutomationAggregate.Entities;

public sealed class AutomationAction : Entity<AutomationActionId>
{
    private readonly List<AutomationActionParameter> _parameters;
    private readonly List<IntegrationId> _providers;

    public string Identifier { get; private set; }
    public IReadOnlyList<AutomationActionParameter> Parameters => _parameters.AsReadOnly();
    public IReadOnlyList<IntegrationId> Providers => _providers.AsReadOnly();
    public int Rank { get; private set; }

    private AutomationAction(
        AutomationActionId id,
        string identifier,
        int rank,
        List<AutomationActionParameter> parameters,
        List<IntegrationId> providers)
        : base(id)
    {
        Identifier = identifier;
        _parameters = parameters;
        _providers = providers;
        Rank = rank;
    }

    public static AutomationAction Create(string identifier, int rank, List<AutomationActionParameter> parameters,
        List<IntegrationId> providers)
    {
        return new AutomationAction(
            AutomationActionId.CreateUnique(),
            identifier,
            rank,
            parameters,
            providers);
    }
}
