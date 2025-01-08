using System.Reflection;

using Humanizer;

using Zeus.Common.Domain.AutomationAggregate.Entities;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Daemon.Application.Attributes;

namespace Zeus.Daemon.Application.Extensions;

public static class ParameterInfoExtensions
{
    public static bool IsValidActionHandlerParameter(this ParameterInfo parameter)
    {
        var fromParameter = parameter.GetCustomAttribute<FromParametersAttribute>() != null;
        var fromIntegrations = parameter.GetCustomAttribute<FromIntegrationsAttribute>() != null;
        var automationId = parameter.ParameterType.IsAssignableTo(typeof(AutomationId));
        var cancellationToken = parameter.ParameterType.IsAssignableTo(typeof(CancellationToken));

        return fromParameter || fromIntegrations || automationId || cancellationToken;
    }

    public static bool IsValidOnTriggerRegisterMethodParameter(this ParameterInfo parameter)
    {
        var fromParameter = parameter.GetCustomAttribute<FromParametersAttribute>() != null;
        var fromIntegrations = parameter.GetCustomAttribute<FromIntegrationsAttribute>() != null;
        var automationId = parameter.ParameterType.IsAssignableTo(typeof(AutomationId));
        var trigger = parameter.ParameterType.IsAssignableTo(typeof(AutomationTrigger));
        var cancellationToken = parameter.ParameterType.IsAssignableTo(typeof(CancellationToken));

        return fromParameter || fromIntegrations || automationId || cancellationToken || trigger;
    }

    public static bool IsValidOnTriggerRemoveMethodParameter(this ParameterInfo parameter)
    {
        var automationId = parameter.ParameterType.IsAssignableTo(typeof(AutomationId));
        var cancellationToken = parameter.ParameterType.IsAssignableTo(typeof(CancellationToken));

        return automationId || cancellationToken;
    }

    public static string? GetParameterIdentifierFromAttribute(this ParameterInfo parameter)
    {
        var attribute = parameter.GetCustomAttribute<FromParametersAttribute>();

        return attribute != null ? (attribute.Identifier ?? parameter.Name).Pascalize() : null;
    }

    public static bool HasAttribute<T>(this ParameterInfo parameter) where T : Attribute
    {
        return parameter.GetCustomAttribute<T>() != null;
    }
}
