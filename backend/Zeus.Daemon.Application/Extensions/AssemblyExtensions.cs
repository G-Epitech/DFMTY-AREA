using System.Reflection;

using Zeus.Daemon.Application.Attributes;

namespace Zeus.Daemon.Application.Extensions;

public static class AssemblyExtensions
{
    public static IList<Type> GetActionHandlersHostingTypes(this Assembly assembly)
    {
        return assembly.GetTypes()
            .Where(t => !t.IsAbstract && t.ContainsActionHandlerMethods())
            .ToList();
    }
}
