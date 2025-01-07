using Microsoft.Extensions.DependencyInjection;

using Zeus.Common.Domain.AutomationAggregate.Entities;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.ProvidersSettings;
using Zeus.Daemon.Application.Interfaces;

namespace Zeus.Daemon.Application.Services;

public class TriggersRegistry : ITriggersRegistry
{
    private readonly Dictionary<string, ITriggerHandler> _handlers = new();

    public TriggersRegistry(IServiceProvider serviceProvider, ProvidersSettings providersSettings)
    {
        RegisterHandlers(serviceProvider
            .GetServices<ITriggerHandler>()
            .ToList()
            .AsReadOnly()
        );
        EnsureEveryTriggerHasHandler(providersSettings);
    }

    public Task<bool> RegisterAsync(AutomationTrigger trigger, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveAsync(AutomationTriggerId triggerId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    private void RegisterHandlers(IReadOnlyList<ITriggerHandler> handlers)
    {
        _handlers.EnsureCapacity(handlers.Count);

        foreach (var handler in handlers)
        {
            RegisterHandler(handler);
        }
    }

    private void RegisterHandler(ITriggerHandler handler)
    {
        if (_handlers.TryAdd(handler.Identifier, handler))
        {
            return;
        }

        var currentHandlerClass = _handlers[handler.Identifier].GetType().Name;
        var candidateHandlerClass = handler.GetType().Name;
        throw new InvalidOperationException(
            $"Trigger handler with identifier '{handler.Identifier}' already registered. Duplicated handlers are: {currentHandlerClass} (current), {candidateHandlerClass} (candidate)");
    }

    private void EnsureEveryTriggerHasHandler(ProvidersSettings providersSettings)
    {
        foreach (var triggerIdentifier in providersSettings.AllTriggerIdentifiers)
        {
            if (!_handlers.ContainsKey(triggerIdentifier))
            {
                throw new InvalidOperationException($"Trigger '{triggerIdentifier}' has no handler registered");
            }
        }
    }
}
