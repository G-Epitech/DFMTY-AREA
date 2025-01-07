namespace Zeus.Daemon.Application.Interfaces.HandlerProviders;

public interface ITriggerHandlersProvider
{
    public ITriggerHandler GetHandler(string triggerIdentifier);
}
