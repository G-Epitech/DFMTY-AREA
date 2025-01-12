using MediatR;

using Zeus.Common.Domain.AutomationAggregate.Events;

namespace Zeus.Api.Application.Automations.Events.AutomationCreated;

public class AutomationCreatedEventHandler : INotificationHandler<AutomationCreatedEvent>
{
    public Task Handle(AutomationCreatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Automation {notification.Automation.Id.Value.ToString()} created");
        return Task.CompletedTask;
    }
}
