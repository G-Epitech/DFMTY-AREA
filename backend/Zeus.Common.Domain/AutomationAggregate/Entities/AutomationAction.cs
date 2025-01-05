using Zeus.BuildingBlocks.Domain.Models;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;

namespace Zeus.Common.Domain.AutomationAggregate.Entities;

public sealed class AutomationAction : Entity<AutomationActionId>
{
    private readonly List<AutomationActionParameter> _parameters;
    private readonly List<IntegrationId> _providers;

    public string Identifier { get; private set; }
    public IReadOnlyList<AutomationActionParameter> Parameters => _parameters.AsReadOnly();
    public IReadOnlyList<IntegrationId> Providers => _providers.AsReadOnly();
    public int Rank { get; private set; }

    public AutomationAction(
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

    public static AutomationAction Create(string identifier, int rank, List<AutomationActionParameter> parameters, List<IntegrationId> providers)
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
