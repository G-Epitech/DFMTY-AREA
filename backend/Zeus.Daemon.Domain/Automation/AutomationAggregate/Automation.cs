using Zeus.Common.Domain.Models;
using Zeus.Daemon.Domain.Automation.AutomationAggregate.Entities;
using Zeus.Daemon.Domain.Automation.AutomationAggregate.ValueObjects;
using Zeus.Daemon.Domain.User.ValueObjects;

namespace Zeus.Daemon.Domain.Automation.AutomationAggregate;

public sealed class Automation : AggregateRoot<AutomationId>
{
    private readonly List<AutomationAction> _actions;

    public string Label { get; private set; }
    public string Description { get; private set; }
    public AutomationTrigger Trigger { get; private set; }
    public IReadOnlyList<AutomationAction> Actions => _actions.AsReadOnly();
    public UserId OwnerId { get; private set; }
    public bool Enabled { get; private set; }

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
            DateTime.UtcNow,
            DateTime.UtcNow,
            enabled);
    }
}
