using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Daemon.Application.Interfaces.HandlerProviders;
using Zeus.Daemon.Application.Interfaces.Registries;

namespace Zeus.Daemon.Application.Services.Registries;

public class TriggersRegistry : ITriggersRegistry
{
    private readonly ITriggerHandlersProvider _handlersProvider;

    public TriggersRegistry(ITriggerHandlersProvider handlersProvider)
    {
        _handlersProvider = handlersProvider;
        Console.WriteLine("TriggersRegistry intialized");
    }

    public async Task<bool> RegisterAsync(Automation automation, CancellationToken cancellationToken = default)
    {
        return await _handlersProvider
            .GetHandler(automation.Trigger.Identifier)
            .RegisterAsync(automation, cancellationToken);
    }

    public Task<bool> RemoveAsync(AutomationId automationId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
