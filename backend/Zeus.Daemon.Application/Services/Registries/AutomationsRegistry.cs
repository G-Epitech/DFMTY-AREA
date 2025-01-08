using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Daemon.Application.Interfaces.Registries;

namespace Zeus.Daemon.Application.Services.Registries;

public class AutomationsRegistry : IAutomationsRegistry
{
    private readonly Dictionary<AutomationId, Automation> _automations = new();
    private readonly ITriggersRegistry _triggersRegistry;

    public AutomationsRegistry(ITriggersRegistry triggersRegistry)
    {
        _triggersRegistry = triggersRegistry;
    }

    public async Task<bool> RegisterAsync(Automation automation, CancellationToken cancellationToken = default)
    {
        var valid = !_automations.ContainsKey(automation.Id)
                    && await _triggersRegistry.RegisterAsync(automation, cancellationToken);

        if (valid)
        {
            _automations.Add(automation.Id, automation);
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
}
