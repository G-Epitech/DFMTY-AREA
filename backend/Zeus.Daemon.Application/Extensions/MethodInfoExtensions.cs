using System.Reflection;

using Zeus.Daemon.Application.Attributes;

namespace Zeus.Daemon.Application.Extensions;

public static class MethodInfoExtensions
{
    public static bool HasAttribute<T>(this MethodInfo methodInfo) where T : Attribute
    {
        return methodInfo.GetCustomAttributes<T>().Any();
    }
    
    public static bool ContainsActionHandlerMethods(this Type type)
    {
        return type
            .GetMethods()
            .Any(m => m.HasAttribute<ActionHandlerAttribute>());
    }
}
