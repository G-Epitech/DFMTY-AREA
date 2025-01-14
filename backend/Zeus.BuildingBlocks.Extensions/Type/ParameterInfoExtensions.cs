using System.Reflection;

using NullableAttribute = System.Runtime.CompilerServices.NullableAttribute;

namespace Zeus.Common.Extensions.Type;

public static class ParameterInfoExtensions
{
    public static bool IsNullable(this ParameterInfo info)
    {
        var type = info.ParameterType;

        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            return true;
        }

        return info.GetCustomAttribute<NullableAttribute>()?.NullableFlags[0] == 2;
    }
}
