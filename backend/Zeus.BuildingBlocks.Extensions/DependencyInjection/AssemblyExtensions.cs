using System.Reflection;

namespace Zeus.Common.Extensions.DependencyInjection;

public static class AssemblyExtensions
{
    public static IList<Type> GetAutoStartedServicesTypes(this Assembly assembly)
    {
        return assembly.GetTypes()
            .Where(t => t.GetCustomAttribute<AutoStartedAttribute>() != null)
            .ToList();
    }
}
