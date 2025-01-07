using System.Reflection;

using Humanizer;

using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Daemon.Application.Attributes;
using Zeus.Daemon.Application.Automations;

namespace Zeus.Daemon.Application.Extensions;

public static class ParameterInfoExtensions
{
    public static bool IsValidActionHandlerParameter(this ParameterInfo parameter)
    {
        var fromParameter = parameter.GetCustomAttribute<FromParameterAttribute>() != null;
        var fromIntegrations = parameter.GetCustomAttribute<FromIntegrationsAttribute>() != null;
        var automationId = parameter.ParameterType.IsAssignableTo(typeof(AutomationId));
        var cancellationToken = parameter.ParameterType.IsAssignableTo(typeof(CancellationToken));
        
        return fromParameter || fromIntegrations || automationId || cancellationToken;
    }
    
    public static bool HasFromParameterAttribute(this ParameterInfo parameter)
    {
        return parameter.GetCustomAttribute<FromParameterAttribute>() != null;
    }
    
    public static string? GetFromParameterIdentifier(this ParameterInfo parameter)
    {
        var attribute = parameter.GetCustomAttribute<FromParameterAttribute>();

        return attribute != null ? (attribute.Identifier ?? parameter.Name).Pascalize() : null;
    }
}
