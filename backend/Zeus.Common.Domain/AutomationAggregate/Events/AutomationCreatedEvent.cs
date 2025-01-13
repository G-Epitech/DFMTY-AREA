using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Common.Domain.AutomationAggregate.Events;

public sealed record AutomationCreatedEvent(
    Automation Automation) : IDomainEvent;
