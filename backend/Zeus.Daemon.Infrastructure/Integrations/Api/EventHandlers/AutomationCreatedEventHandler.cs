using MapsterMapper;

using MassTransit;

using Zeus.Api.Integration.Contracts;
using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.Entities;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;
using Zeus.Daemon.Application.Interfaces.Registries;

namespace Zeus.Daemon.Infrastructure.Integrations.Api.EventHandlers;

public class AutomationCreatedEventHandler : IConsumer<AutomationCreatedEvent>
{
    private readonly IAutomationsRegistry _automationsRegistry;
    private readonly IMapper _mapper;

    public AutomationCreatedEventHandler(IAutomationsRegistry automationsRegistry, IMapper mapper)
    {
        _automationsRegistry = automationsRegistry;
        _mapper = mapper;
    }

    public Task Consume(ConsumeContext<AutomationCreatedEvent> context)
    {
        var automation = _mapper.Map<Automation>(context.Message);

        _automationsRegistry.RegisterAsync(automation);
        return Task.CompletedTask;
    }
}
