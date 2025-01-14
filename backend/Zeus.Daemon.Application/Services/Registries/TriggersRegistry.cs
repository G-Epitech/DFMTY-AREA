using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Daemon.Application.Interfaces.HandlerProviders;
using Zeus.Daemon.Application.Interfaces.Registries;
using Zeus.Daemon.Domain.Automations;

namespace Zeus.Daemon.Application.Services.Registries;

public class TriggersRegistry : ITriggersRegistry
{
    private readonly ITriggerHandlersProvider _handlersProvider;

    public TriggersRegistry(ITriggerHandlersProvider handlersProvider)
    {
        _handlersProvider = handlersProvider;
    }

    public async Task<bool> RegisterAsync(RegistrableAutomation registrable, CancellationToken cancellationToken = default)
    {
        var handler = _handlersProvider.GetHandler(registrable.Automation.Trigger.Identifier);

        return await handler.RegisterAsync(registrable, cancellationToken);
    }

    public async Task<bool> RemoveAsync(Automation automation, CancellationToken cancellationToken = default)
    {
        var handler = _handlersProvider.GetHandler(automation.Trigger.Identifier);

        return await handler.RemoveAsync(automation.Id, cancellationToken);
    }
}
