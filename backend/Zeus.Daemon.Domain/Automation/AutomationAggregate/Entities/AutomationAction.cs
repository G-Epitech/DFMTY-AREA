using Zeus.Common.Domain.Models;
using Zeus.Daemon.Domain.Automation.AutomationAggregate.ValueObjects;
using Zeus.Daemon.Domain.IntegrationAggregate.ValueObjects;

namespace Zeus.Daemon.Domain.Automation.AutomationAggregate.Entities;

public sealed class AutomationAction : Entity<AutomationActionId>
{
    private readonly List<DynamicParameter> _parameters = [];
    private readonly List<IntegrationId> _providers = [];

    private string Identifier { get; set; }
    private int Rank { get; set; }

    public IReadOnlyList<DynamicParameter> Parameters => _parameters.AsReadOnly();
    public IReadOnlyList<IntegrationId> Providers => _providers.AsReadOnly();

    private AutomationAction(
        AutomationActionId id,
        string identifier,
        int rank,
        List<DynamicParameter> parameters,
        List<IntegrationId> providers)
        : base(id)
    {
        Identifier = identifier;
        Rank = rank;
        _parameters = parameters;
        _providers = providers;
    }

    public static AutomationAction Create(string identifier, int rank, List<DynamicParameter> parameters,
        List<IntegrationId> providers)
    {
        return new AutomationAction(
            AutomationActionId.CreateUnique(),
            identifier,
            rank,
            parameters,
            providers);
    }

#pragma warning disable CS8618
    private AutomationAction()
    {
    }
#pragma warning restore CS8618
}
