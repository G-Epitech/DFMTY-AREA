﻿using System.Diagnostics.CodeAnalysis;

using Zeus.BuildingBlocks.Domain.Models;
using Zeus.Common.Domain.AutomationAggregate.Entities;
using Zeus.Common.Domain.AutomationAggregate.Events;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Common.Domain.AutomationAggregate;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.NonPublicConstructors)]
public sealed class Automation : AggregateRoot<AutomationId>
{
    public const int LabelMaxLength = 100;
    public const int DescriptionMaxLength = 255;

    private readonly List<AutomationAction> _actions;
    private bool _enabled;

    public Automation(
        AutomationId id,
        string label,
        string description,
        UserId ownerId,
        AutomationTrigger trigger,
        List<AutomationAction> actions,
        DateTime updatedAt,
        DateTime createdAt,
        bool enabled = true)
        : base(id, updatedAt, createdAt)
    {
        _actions = actions;
        Label = label;
        Description = description;
        Trigger = trigger;
        OwnerId = ownerId;
        _enabled = enabled;
    }

#pragma warning disable CS8618
    private Automation()
    {
    }
#pragma warning restore CS8618

    public string Label { get; private set; }
    public string Description { get; private set; }
    public AutomationTrigger Trigger { get; private set; }
    public IReadOnlyList<AutomationAction> Actions => _actions.AsReadOnly();
    public UserId OwnerId { get; private set; }

    public IReadOnlyList<IntegrationId> Providers => _actions
        .SelectMany(a => a.Providers)
        .Union(Trigger.Providers)
        .Distinct()
        .ToList();

    public bool Enabled
    {
        get => _enabled;
        private set
        {
            if (value == _enabled)
            {
                return;
            }
            _enabled = value;
            AddDomainEvent(new AutomationEnabledStateChangedEvent(this));
        }
    }

    public static Automation Create(
        string label,
        string description,
        UserId ownerId,
        AutomationTrigger trigger,
        List<AutomationAction> actions,
        bool enabled = true)
    {
        var automation = new Automation(
            AutomationId.CreateUnique(),
            label,
            description,
            ownerId,
            trigger,
            actions,
            DateTime.UtcNow,
            DateTime.UtcNow,
            enabled);
        automation.AddDomainEvent(new AutomationCreatedEvent(automation));
        return automation;
    }
}
