using System.Reflection;

namespace Zeus.Daemon.Application.Extensions;

public static class TypeExtensions
{
    public static bool HasAttribute<T>(this Type type) where T : Attribute
    {
        return type.GetCustomAttribute<T>() != null;
    }
}
