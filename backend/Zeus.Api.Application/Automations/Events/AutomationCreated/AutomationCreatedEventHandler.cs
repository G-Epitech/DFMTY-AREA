using MapsterMapper;

using MassTransit;

using MediatR;

using AutomationCreatedDomainEvent = Zeus.Common.Domain.AutomationAggregate.Events.AutomationCreatedEvent;
using AutomationCreatedIntegrationEvent = Zeus.Api.Integration.Contracts.AutomationCreatedEvent;

namespace Zeus.Api.Application.Automations.Events.AutomationCreated;

public class AutomationCreatedEventHandler : INotificationHandler<AutomationCreatedDomainEvent>
{
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;

    public AutomationCreatedEventHandler(IPublishEndpoint publishEndpoint, IMapper mapper)
    {
        _publishEndpoint = publishEndpoint;
        _mapper = mapper;
    }

    public Task Handle(AutomationCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var automation = notification.Automation;
        var integrationEvent = _mapper.Map<AutomationCreatedIntegrationEvent>(automation);

        return _publishEndpoint.Publish(integrationEvent, cancellationToken);
    }
}
