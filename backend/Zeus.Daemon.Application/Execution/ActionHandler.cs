using System.Reflection;

namespace Zeus.Daemon.Application.Execution;

public struct ActionHandler
{
    public required object Target { get; init; }
    public required MethodInfo Method { get; init; }
}
