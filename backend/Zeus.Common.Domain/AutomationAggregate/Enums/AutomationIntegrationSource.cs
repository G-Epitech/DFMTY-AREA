namespace Zeus.Common.Domain.AutomationAggregate.Enums;

[Flags]
public enum AutomationIntegrationSource
{
    Trigger = 1,
    Action = 2,
    Any = Trigger | Action
}
