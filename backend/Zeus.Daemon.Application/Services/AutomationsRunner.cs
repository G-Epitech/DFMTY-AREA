using Microsoft.Extensions.Logging;

using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Daemon.Application.Execution;
using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Application.Interfaces.HandlerProviders;
using Zeus.Daemon.Application.Interfaces.Registries;
using Zeus.Daemon.Domain.Automations;

namespace Zeus.Daemon.Application.Services;

public sealed class AutomationsRunner : IAutomationsRunner
{
    private readonly IActionHandlersProvider _actionHandlersProvider;
    private readonly IAutomationsRegistry _automationsRegistry;
    private readonly IIntegrationsProvider _integrationsProvider;
    private readonly ILogger _logger;

    public AutomationsRunner(
        IAutomationsRegistry automationsRegistry,
        IActionHandlersProvider actionHandlersProvider,
        IIntegrationsProvider integrationsProvider,
        ILogger<AutomationsRunner> logger)
    {
        _automationsRegistry = automationsRegistry;
        _actionHandlersProvider = actionHandlersProvider;
        _integrationsProvider = integrationsProvider;
        _logger = logger;
    }

    public async Task<bool> RunAsync(AutomationId automationId, FactsDictionary facts)
    {
        var automation = _automationsRegistry.GetAutomation(automationId);
        var integrations = await _integrationsProvider.GetActionsIntegrationsByAutomationIdsAsync([automationId]);

        if (automation is null)
        {
            _logger.LogError("Automation {id} could not be started because it was not found", automationId.Value);
            return false;
        }

        var ctx = new AutomationExecutionContext(_actionHandlersProvider, automation, integrations, facts);

        ctx.Run();
        return true;
    }

    public async Task<Dictionary<AutomationId, bool>> RunManyAsync(IReadOnlyList<AutomationId> automationIds, FactsDictionary facts)
    {
        var automations = _automationsRegistry.GetAutomations(automationIds);
        var results = automationIds.ToDictionary(id => id, _ => true);
        var integrations = await _integrationsProvider.GetActionsIntegrationsByAutomationIdsAsync(automationIds);

        foreach (var automation in automations)
        {
            var automationIntegrations = GetAutomationIntegrations(automation, integrations.Values);
            var ctx = new AutomationExecutionContext(_actionHandlersProvider, automation, automationIntegrations, facts);

            ctx.Run();
        }

        var notFound = automationIds.Except(automations.Select(a => a.Id)).ToList();

        foreach (var id in notFound)
        {
            _logger.LogError("Automation {id} could not be started because it was not found", id.Value);
            results[id] = false;
        }

        return results;
    }

    private static Dictionary<IntegrationId, Integration> GetAutomationIntegrations(Automation automation, IReadOnlyCollection<Integration> integrations)
    {
        var automationIntegrations = new Dictionary<IntegrationId, Integration>();
        var dependencies = automation.Dependencies;

        foreach (var integration in integrations)
        {
            if (dependencies.Contains(integration.Id))
            {
                automationIntegrations[integration.Id] = integration;
            }
        }

        return automationIntegrations;
    }
}
