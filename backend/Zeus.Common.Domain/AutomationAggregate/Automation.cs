using Zeus.BuildingBlocks.Domain.Models;
using Zeus.Common.Domain.AutomationAggregate.Entities;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Common.Domain.AutomationAggregate;

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

#pragma warning disable CS8618
    private Automation()
    {
    }
#pragma warning restore CS8618
}
