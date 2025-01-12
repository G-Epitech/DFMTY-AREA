using Mapster;

using MapsterMapper;

using MassTransit;

using MediatR;

using Zeus.Api.Integration.Contracts;

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

        /*new AutomationCreatedIntegrationEvent()
        {
            Id = automation.Id.Value,
            Label = automation.Label,
            Description = automation.Description,
            OwnerId = automation.OwnerId.Value,
            Enabled = automation.Enabled,
            Actions = automation.Actions.Select(x => new AutomationCreatedEventAction
            {
                Id = x.Id.Value,
                Identifier = x.Identifier,
                Parameters =
                    x.Parameters.Select(p => new AutomationCreatedEventAction.Parameter { Identifier = p.Identifier, Value = p.Value, Type = p.Type }).ToList(),
                Providers = x.Providers.Select(p => p.Value).ToList(),
                Rank = x.Rank
            }).ToList(),
            Trigger = new AutomationCreatedEventTrigger
            {
                Id = automation.Trigger.Id.Value,
                Identifier = automation.Trigger.Identifier,
                Parameters = automation.Trigger.Parameters.Select(p => new AutomationCreatedEventTrigger.Parameter { Identifier = p.Identifier, Value = p.Value })
                    .ToList(),
                Providers = automation.Trigger.Providers.Select(p => p.Value).ToList()
            },
            CreatedAt = automation.CreatedAt,
            UpdatedAt = automation.UpdatedAt
        };*/

        return _publishEndpoint.Publish(integrationEvent, cancellationToken);
    }
}
