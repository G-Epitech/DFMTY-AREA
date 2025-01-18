using MapsterMapper;

using MassTransit;

using Microsoft.Extensions.Logging;

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
    private readonly ILogger _logger;

    public AutomationCreatedEventHandler(
        IAutomationsRegistry automationsRegistry,
        IIntegrationsProvider integrationsProvider,
        ILogger<AutomationCreatedEventHandler> logger,
        IMapper mapper)
    {
        _automationsRegistry = automationsRegistry;
        _mapper = mapper;
        _integrationsProvider = integrationsProvider;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<AutomationCreatedEvent> context)
    {
        try
        {
            var automation = _mapper.Map<Automation>(context.Message);
            var integrations = await _integrationsProvider.GetTriggerIntegrationsByAutomationIdAsync(automation.Id);

            try
            {
                _logger.LogInformation("{AutomationId} loaded with {IntegrationId}", automation.Id, integrations.Values.Count);
                await _automationsRegistry.RegisterAsync(new RegistrableAutomation { Automation = automation, TriggerIntegrations = integrations.Values.ToList() },
                    context.CancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured when registering automation {AutomationId}", automation.Id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while handling automation created event");
        }
    }
}
