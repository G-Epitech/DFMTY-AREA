using System.Reflection;

using Zeus.Common.Extensions.Type;

namespace Zeus.Daemon.Application.Utils;

public static class ParameterUtils
{
    public static FromIntegrationsParameterInfo GetFromIntegrationsParameterInfo(this ParameterInfo parameterInfo)
    {
        var genericType = parameterInfo.ParameterType.IsGenericType ? parameterInfo.ParameterType.GetGenericTypeDefinition() : null;
        var genericArg = parameterInfo.ParameterType.GetGenericArguments().FirstOrDefault();
        var genericList = genericType is not null ? typeof(IList<>).MakeGenericType(genericArg!) : null;
        var genericReadOnlyList = genericType is not null ? typeof(IReadOnlyList<>).MakeGenericType(genericArg!) : null;
        var isGenericCollection = genericList is not null && genericReadOnlyList is not null &&
                                  (genericList.IsAssignableTo(parameterInfo.ParameterType) || genericReadOnlyList.IsAssignableTo(parameterInfo.ParameterType));

        return new FromIntegrationsParameterInfo
        {
            IsCollection = isGenericCollection,
            Type = isGenericCollection ? genericArg! : parameterInfo.ParameterType,
            Optional = parameterInfo.IsNullable()
        };
    }

    public struct FromIntegrationsParameterInfo
    {
        public required bool IsCollection { get; init; }
        public required Type Type { get; init; }
        public required bool Optional { get; init; }
    }
}
