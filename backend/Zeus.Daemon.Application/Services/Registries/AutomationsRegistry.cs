using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Daemon.Application.Interfaces.Registries;
using Zeus.Daemon.Domain.Automations;

namespace Zeus.Daemon.Application.Services.Registries;

public class AutomationsRegistry : IAutomationsRegistry
{
    private readonly Dictionary<AutomationId, Automation> _automations = new();
    private readonly ITriggersRegistry _triggersRegistry;

    public AutomationsRegistry(ITriggersRegistry triggersRegistry)
    {
        _triggersRegistry = triggersRegistry;
    }

    public async Task<bool> RegisterAsync(RegistrableAutomation registrable, CancellationToken cancellationToken = default)
    {
        var automation = registrable.Automation;
        var exists = _automations.ContainsKey(automation.Id);

        if (exists && !await RemoveAsync(registrable.Automation.Id, cancellationToken))
        {
            return false;
        }

        var valid = await _triggersRegistry.RegisterAsync(registrable, cancellationToken);

        if (valid)
        {
            _automations[automation.Id] = automation;
        }
        return valid;
    }

    public async Task<bool> RemoveAsync(AutomationId automationId, CancellationToken cancellationToken = default)
    {
        var valid = _automations.TryGetValue(automationId, out var automation)
                    && await _triggersRegistry.RemoveAsync(automation, cancellationToken);

        if (valid)
        {
            _automations.Remove(automationId);
        }
        return valid;
    }

    public Automation? GetAutomation(AutomationId automationId)
    {
        return _automations.GetValueOrDefault(automationId);
    }

    public IReadOnlyList<Automation> GetAutomations(IReadOnlyList<AutomationId> automationIds)
    {
        return _automations.Values
            .Where(k => automationIds.Contains(k.Id))
            .ToList();
    }
}
