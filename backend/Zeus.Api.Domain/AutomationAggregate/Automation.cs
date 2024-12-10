using Zeus.Api.Domain.AutomationAggregate.Entities;
using Zeus.Api.Domain.AutomationAggregate.ValueObjects;
using Zeus.Api.Domain.UserAggregate.ValueObjects;
using Zeus.Common.Domain.Models;

namespace Zeus.Api.Domain.AutomationAggregate;

public sealed class Automation : AggregateRoot<AutomationId>
{
    private readonly List<AutomationAction> _actions;

    public string Label { get; private set; }
    public string Description { get; private set; }
    public AutomationTrigger Trigger { get; private set; }
    public IReadOnlyList<AutomationAction> Actions => _actions.AsReadOnly();
    public UserId OwnerId { get; private set; }
    public bool Enabled { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Automation(
        AutomationId id,
        string label,
        string description,
        UserId ownerId,
        AutomationTrigger trigger,
        List<AutomationAction> actions,
        bool enabled = true)
        : base(id)
    {
        _actions = actions;
        Label = label;
        Description = description;
        Trigger = trigger;
        OwnerId = ownerId;
        Enabled = enabled;
    }

    public static Automation Create(
        string label,
        string description,
        UserId ownerId,
        AutomationTrigger trigger,
        List<AutomationAction> actions,
        bool enabled = true)
    {
        return new Automation(
            AutomationId.CreateUnique(),
            label,
            description,
            ownerId,
            trigger,
            actions,
            enabled);
    }

#pragma warning disable CS8618
    private Automation()
    {
    }
#pragma warning restore CS8618
}
