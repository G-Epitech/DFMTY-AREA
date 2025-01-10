using Zeus.Common.Extensions.DependencyInjection;
using Zeus.Daemon.Application.Execution;

namespace Zeus.Daemon.Application.Interfaces.HandlerProviders;

[AutoStarted]
public interface ITriggerHandlersProvider
{
    public TriggerHandler GetHandler(string triggerIdentifier);
}
