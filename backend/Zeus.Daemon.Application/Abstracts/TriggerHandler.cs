using System.Reflection;

using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.Entities;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Daemon.Application.Attributes;
using Zeus.Daemon.Application.Interfaces;

namespace Zeus.Daemon.Application.Abstracts;

public abstract class TriggerHandler : ITriggerHandler
{
    protected readonly Dictionary<AutomationId, AutomationTrigger> _triggers = [];

    public string Identifier { get; private set; }

    protected TriggerHandler()
    {
        Identifier = FetchIdentifier();
    }

    public async Task<bool> RegisterAsync(Automation automation, CancellationToken cancellationToken = default)
    {
        var valid = _triggers.TryAdd(automation.Id, automation.Trigger) && await OnRegisterAsync(automation, cancellationToken);

        if (!valid)
        {
            _triggers.Remove(automation.Id);
        }
        return valid;
    }

    public async Task<bool> RemoveAsync(AutomationId automationId, CancellationToken cancellationToken = default)
    {
        if (!_triggers.TryGetValue(automationId, out var trigger))
        {
            return true;
        }

        if (!await OnRemoveAsync(trigger, cancellationToken))
        {
            return false;
        }
        _triggers.Remove(automationId);
        return true;
    }

    protected virtual Task<bool> OnRegisterAsync(Automation automation, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(true);
    }

    protected virtual Task<bool> OnRemoveAsync(AutomationTrigger trigger, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(true);
    }

    private string FetchIdentifier()
    {
        var triggerIdentifierAttribute = GetType().GetCustomAttribute<TriggerIdentifierAttribute>();
        if (triggerIdentifierAttribute is null)
        {
            throw new InvalidOperationException($"TriggerHandler {GetType().Name} does not have a TriggerIdentifierAttribute");
        }
        return triggerIdentifierAttribute.Identifier;
    }
}
