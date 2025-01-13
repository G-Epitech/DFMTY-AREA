using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Daemon.Application.Execution;
using Zeus.Daemon.Application.Interfaces.HandlerProviders;
using Zeus.Daemon.Application.Interfaces.Registries;

namespace Zeus.Daemon.Application.Services.Registries;

public class TriggersRegistry : ITriggersRegistry
{
    private readonly ITriggerHandlersProvider _handlersProvider;

    public TriggersRegistry(ITriggerHandlersProvider handlersProvider)
    {
        _handlersProvider = handlersProvider;
    }

    public async Task<bool> RegisterAsync(Automation automation, CancellationToken cancellationToken = default)
    {
        var handler = _handlersProvider.GetHandler(automation.Trigger.Identifier);

        return await handler.RegisterAsync(automation, cancellationToken);
    }

    public async Task<bool> RemoveAsync(Automation automation, CancellationToken cancellationToken = default)
    {
        var handler = _handlersProvider.GetHandler(automation.Trigger.Identifier);
        var invoker = new TriggerHandlerInvoker(handler);

        return await invoker.RemoveAsync(automation.Id, cancellationToken);
    }
}
