using System.Reflection;

using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;

namespace Zeus.Daemon.Application.Execution;

public readonly struct TriggerHandler
{
    public required object Target { get; init; }
    public required MethodInfo OnRegisterMethod { get; init; }
    public required MethodInfo OnRemoveMethod { get; init; }

    public async Task<bool> RegisterAsync(Automation automation, CancellationToken cancellationToken = default)
    {
        var invoker  = new TriggerHandlerInvoker(this);

        return await invoker.RegisterAsync(automation, cancellationToken);
    }
    
    public async Task<bool> RemoveAsync(AutomationId automationId, CancellationToken cancellationToken = default)
    {
        var invoker = new TriggerHandlerInvoker(this);

        return await invoker.RemoveAsync(automationId, cancellationToken);
    }
}
