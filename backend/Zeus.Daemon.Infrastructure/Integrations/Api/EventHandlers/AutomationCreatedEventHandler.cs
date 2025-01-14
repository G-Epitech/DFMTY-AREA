using MapsterMapper;

using MassTransit;

using Zeus.Api.Integration.Contracts;
using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Application.Interfaces.Registries;
using Zeus.Daemon.Domain.Automations;

namespace Zeus.Daemon.Infrastructure.Integrations.Api.EventHandlers;

public class AutomationCreatedEventHandler : IConsumer<AutomationCreatedEvent>
{
    private readonly IAutomationsRegistry _automationsRegistry;
    private readonly IIntegrationsProvider _integrationsProvider;
    private readonly IMapper _mapper;

    public AutomationCreatedEventHandler(
        IAutomationsRegistry automationsRegistry,
        IIntegrationsProvider integrationsProvider,
        IMapper mapper)
    {
        _automationsRegistry = automationsRegistry;
        _mapper = mapper;
        _integrationsProvider = integrationsProvider;
    }

    public async Task Consume(ConsumeContext<AutomationCreatedEvent> context)
    {
        var automation = _mapper.Map<Automation>(context.Message);
        var integrations = await _integrationsProvider.GetTriggerIntegrationsByAutomationIdAsync(automation.Id);

        await _automationsRegistry.RegisterAsync(new RegistrableAutomation { Automation = automation, TriggerIntegrations = integrations.Values.ToList() },
            context.CancellationToken);
    }
}
