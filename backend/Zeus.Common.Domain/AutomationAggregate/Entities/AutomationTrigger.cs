﻿using System.Diagnostics.CodeAnalysis;

using Zeus.BuildingBlocks.Domain.Models;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;

namespace Zeus.Common.Domain.AutomationAggregate.Entities;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.NonPublicConstructors)]
public sealed class AutomationTrigger : Entity<AutomationTriggerId>
{
    private readonly List<AutomationTriggerParameter> _parameters;
    private readonly List<IntegrationId> _providers;

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

#pragma warning disable CS8618
    private AutomationTrigger()
    {
    }
#pragma warning restore CS8618

    public string Identifier { get; private set; }
    public IReadOnlyList<AutomationTriggerParameter> Parameters => _parameters.AsReadOnly();
    public IReadOnlyList<IntegrationId> Providers => _providers.AsReadOnly();

    public static AutomationTrigger Create(string identifier, List<AutomationTriggerParameter> parameters, List<IntegrationId> providers)
    {
        return new AutomationTrigger(
            AutomationTriggerId.CreateUnique(),
            identifier,
            parameters,
            providers);
    }
}
