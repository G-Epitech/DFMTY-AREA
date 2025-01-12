using System.Reflection;

using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.Entities;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Daemon.Application.Attributes;
using Zeus.Daemon.Application.Extensions;

namespace Zeus.Daemon.Application.Execution;

public class TriggerHandlerInvoker
{
    private readonly TriggerHandler _handler;

    public TriggerHandlerInvoker(TriggerHandler handler)
    {
        _handler = handler;
    }

    public Task<bool> RegisterAsync(Automation automation, CancellationToken cancellationToken = default)
    {
        var parameters = GetOnRegisterMethodParameters(_handler.OnRegisterMethod, automation, cancellationToken);

        var res = _handler.OnRegisterMethod.Invoke(_handler.Target, parameters);
        if (res is Task<bool> task)
        {
            return task;
        }
        return Task.FromResult(false);
    }

    public Task<bool> RemoveAsync(AutomationId automationId, CancellationToken cancellationToken = default)
    {
        var parameters = GetOnRemoveMethodParameters(_handler.OnRegisterMethod, automationId, cancellationToken);

        var res = _handler.OnRegisterMethod.Invoke(_handler.Target, parameters);
        if (res is Task<bool> task)
        {
            return task;
        }
        return Task.FromResult(false);
    }

    private static object GetOnRegisterParameterValue(string identifier, Type destType, AutomationTrigger trigger)
    {
        var parameter = trigger.Parameters.FirstOrDefault(p => p.Identifier == identifier);

        if (parameter is null)
        {
            throw new InvalidOperationException($"Parameter with identifier '{identifier}' not found");
        }

        try
        {
            return destType switch
            {
                _ when destType.IsAssignableTo(typeof(int)) => int.Parse(parameter.Value),
                _ when destType.IsAssignableTo(typeof(string)) => parameter.Value,
                _ when destType.IsAssignableTo(typeof(bool)) => bool.Parse(parameter.Value),
                _ when destType.IsAssignableTo(typeof(DateTime)) => DateTime.Parse(parameter.Value),
                _ when destType.IsAssignableTo(typeof(float)) => float.Parse(parameter.Value),
                _ when destType.IsAssignableTo(typeof(object)) => Convert.ChangeType(parameter.Value, destType),
                _ => throw new InvalidOperationException($"Parameter with identifier '{identifier}' has invalid value")
            };
        }
        catch
        {
            throw new InvalidOperationException($"Parameter with identifier '{identifier}' has invalid value");
        }
    }

    private object[] GetOnRegisterMethodParameters(MethodInfo method, Automation automation, CancellationToken cancellationToken)
    {
        var parameters = method.GetParameters();
        var result = new object[parameters.Length];

        foreach (var parameter in parameters)
        {
            var parameterIdentifier = parameter.GetParameterIdentifierFromAttribute();
            var hasIntegrationAttribute = parameter.HasAttribute<FromIntegrationsAttribute>();

            if (parameterIdentifier is not null)
            {
                result[parameter.Position] = GetOnRegisterParameterValue(parameterIdentifier, parameter.ParameterType, automation.Trigger);
            }
            else if (hasIntegrationAttribute)
            {
                // TODO: Implement integration value
                //result[parameter.Position] = GetIntegrationValue(parameter.ParameterType);
            }
            else if (parameter.ParameterType.IsAssignableTo(typeof(CancellationToken)))
            {
                result[parameter.Position] = cancellationToken;
            }
            else if (parameter.ParameterType.IsAssignableTo(typeof(AutomationId)))
            {
                result[parameter.Position] = automation.Id;
            }
            else if (parameter.ParameterType.IsAssignableTo(typeof(AutomationTrigger)))
            {
                result[parameter.Position] = automation.Trigger;
            }
            else
            {
                result[parameter.Position] = 0;
            }
        }
        return result;
    }

    private object[] GetOnRemoveMethodParameters(MethodInfo method, AutomationId automationId, CancellationToken cancellationToken)
    {
        var parameters = method.GetParameters();
        var result = new object[parameters.Length];

        foreach (var parameter in parameters)
        {
            if (parameter.ParameterType.IsAssignableTo(typeof(CancellationToken)))
            {
                result[parameter.Position] = cancellationToken;
            }
            else if (parameter.ParameterType.IsAssignableTo(typeof(AutomationId)))
            {
                result[parameter.Position] = automationId;
            }
            else
            {
                result[parameter.Position] = 0;
            }
        }
        return result;
    }
}
