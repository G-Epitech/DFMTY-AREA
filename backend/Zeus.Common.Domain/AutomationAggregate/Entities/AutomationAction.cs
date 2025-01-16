using System.Diagnostics.CodeAnalysis;

using Zeus.BuildingBlocks.Domain.Models;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;

namespace Zeus.Common.Domain.AutomationAggregate.Entities;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.NonPublicConstructors)]
public sealed class AutomationAction : Entity<AutomationActionId>
{
    private readonly List<AutomationActionParameter> _parameters;
    private readonly List<IntegrationId> _dependencies;

    public AutomationAction(
        AutomationActionId id,
        string identifier,
        int rank,
        List<AutomationActionParameter> parameters,
        List<IntegrationId> dependencies)
        : base(id)
    {
        Identifier = identifier;
        _parameters = parameters;
        _dependencies = dependencies;
        Rank = rank;
    }

#pragma warning disable CS8618
    private AutomationAction()
    {
    }
#pragma warning restore CS8618

    public string Identifier { get; private set; }
    public IReadOnlyList<AutomationActionParameter> Parameters => _parameters.AsReadOnly();
    public IReadOnlyList<IntegrationId> Dependencies => _dependencies.AsReadOnly();
    public int Rank { get; private set; }

    public static AutomationAction Create(string identifier, int rank, List<AutomationActionParameter> parameters, List<IntegrationId> dependencies)
    {
        return new AutomationAction(
            AutomationActionId.CreateUnique(),
            identifier,
            rank,
            parameters,
            dependencies);
    }
}
