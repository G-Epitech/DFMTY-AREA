using Microsoft.Extensions.DependencyInjection;

using Zeus.Common.Domain.ProvidersSettings;
using Zeus.Daemon.Application.Interfaces;
using Zeus.Daemon.Application.Interfaces.HandlerProviders;

namespace Zeus.Daemon.Application.Services.HandlerProviders;

public sealed class TriggerHandlersProvider: ITriggerHandlersProvider
{
    private readonly Dictionary<string, ITriggerHandler> _handlers = new();

    public TriggerHandlersProvider(IServiceProvider serviceProvider, ProvidersSettings providersSettings)
    {
        RegisterHandlers(serviceProvider
            .GetServices<ITriggerHandler>()
            .ToList()
            .AsReadOnly()
        );
        EnsureEveryTriggerHasHandler(providersSettings);
        Console.WriteLine("TriggerHandlersProvider initialized");
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
    
    public ITriggerHandler GetHandler(string triggerIdentifier)
    {
        if (_handlers.TryGetValue(triggerIdentifier, out var handler))
        {
            return handler;
        }
        throw new InvalidOperationException($"Trigger handler with identifier '{triggerIdentifier}' not found");
    }
}
