using System.Reflection;

namespace Zeus.Daemon.Application.Interfaces.HandlerProviders;

public struct ActionHandler
{
    public object Target { get; init; }
    public MethodInfo Method { get; init; }
}

public interface IActionHandlersProvider
{
    public ActionHandler GetHandlerTarget(string actionIdentifier);
}
