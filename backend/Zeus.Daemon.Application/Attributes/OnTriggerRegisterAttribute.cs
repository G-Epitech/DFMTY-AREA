using System.Reflection;

namespace Zeus.Daemon.Application.Attributes;

[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class OnTriggerRegisterAttribute : Attribute;

public static partial class TypeExtensions
{
    public static bool IsOnTriggerRegisterMethod(this MethodInfo methodInfo)
    {
        return methodInfo.GetCustomAttributes<OnTriggerRegisterAttribute>().Any();
    }
}
