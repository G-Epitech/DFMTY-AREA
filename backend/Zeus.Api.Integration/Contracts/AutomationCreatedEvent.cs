using Zeus.Common.Domain.AutomationAggregate.Enums;

namespace Zeus.Api.Integration.Contracts;

public record AutomationCreatedEventTrigger
{
    public required Guid Id;
    public required string Identifier;
    public required Parameter[] Parameters;
    public required Guid[] Dependencies;

    public record Parameter
    {
        public required string Identifier;
        public required string Value;
    }
}

public record AutomationCreatedEventAction
{
    public required Guid Id;
    public required string Identifier;
    public required Parameter[] Parameters;
    public required Guid[] Dependencies;
    public required int Rank;

    public record Parameter
    {
        public required string Identifier;
        public required AutomationActionParameterType Type;
        public required string Value;
    }
}

public record AutomationCreatedEvent
{
    public required AutomationCreatedEventAction[] Actions;
    public required DateTime CreatedAt;
    public required string Description;
    public required bool Enabled;
    public required Guid Id;
    public required string Label;
    public required Guid OwnerId;
    public required AutomationCreatedEventTrigger Trigger;
    public required DateTime UpdatedAt;
}
