using Zeus.Common.Domain.Models;
using Zeus.Daemon.Domain.Automation.AutomationAggregate.Entities;
using Zeus.Daemon.Domain.Automation.AutomationAggregate.ValueObjects;
using Zeus.Daemon.Domain.User.ValueObjects;

namespace Zeus.Daemon.Domain.Automation.AutomationAggregate;

public sealed class Automation : AggregateRoot<AutomationId, Guid>
{
    private readonly List<AutomationAction> _actions;

    public string Label { get; private set; }
    public string Description { get; private set; }
    public AutomationTrigger Trigger { get; private set; }
    public IReadOnlyList<AutomationAction> Actions => _actions.AsReadOnly();
    public UserId OwnerId { get; private set; }

    private Automation(
        AutomationId id,
        string label,
        string description,
        UserId ownerId,
        AutomationTrigger trigger,
        List<AutomationAction> actions)
        : base(id)
    {
        Label = label;
        Description = description;
        Trigger = trigger;
        OwnerId = ownerId;
        _actions = actions;
    }

    public static Automation Create(
        string label,
        string description,
        UserId ownerId,
        AutomationTrigger trigger,
        List<AutomationAction> actions)
    {
        return new Automation(
            AutomationId.CreateUnique(),
            label,
            description,
            ownerId,
            trigger,
            actions);
    }

#pragma warning disable CS8618
    private Automation()
    {
    }
#pragma warning restore CS8618
}
