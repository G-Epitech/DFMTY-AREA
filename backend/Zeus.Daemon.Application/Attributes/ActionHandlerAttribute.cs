using System.Reflection;

namespace Zeus.Daemon.Application.Attributes;

[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class ActionHandlerAttribute : Attribute
{
    public string Identifier { get; }

    public ActionHandlerAttribute(string identifier)
    {
        Identifier = identifier;
    }
}

public static partial class TypeExtensions
{
    public static bool ContainsActionHandlerMethods(this Type type)
    {
        return type
            .GetMethods()
            .Any(m => m.GetCustomAttributes<ActionHandlerAttribute>().Any());
    }

    public static bool IsActionHandlerMethod(this MethodInfo methodInfo)
    {
        return methodInfo.GetCustomAttributes<ActionHandlerAttribute>().Any();
    }
}
