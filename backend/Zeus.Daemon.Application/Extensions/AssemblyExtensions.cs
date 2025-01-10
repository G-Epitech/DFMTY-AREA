using System.Reflection;

using Zeus.Daemon.Application.Attributes;

namespace Zeus.Daemon.Application.Extensions;

public static class AssemblyExtensions
{
    public static IList<Type> GetActionHandlersHostingTypes(this Assembly assembly)
    {
        return assembly.GetTypes()
            .Where(t => t is { IsAbstract: false } && t.ContainsActionHandlerMethods())
            .ToList();
    }

    public static IList<Type> GetTriggerHandlersTypes(this Assembly assembly)
    {
        return assembly.GetTypes()
            .Where(t => t is { IsAbstract: false } && t.HasAttribute<TriggerHandlerAttribute>())
            .ToList();
    }
}
