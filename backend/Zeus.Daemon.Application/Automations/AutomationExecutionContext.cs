using System.Reflection;

using Humanizer;

using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.Entities;
using Zeus.Common.Domain.AutomationAggregate.Enums;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Daemon.Application.Attributes;
using Zeus.Daemon.Application.Extensions;
using Zeus.Daemon.Application.Interfaces.HandlerProviders;
using Zeus.Daemon.Domain.Automations;

namespace Zeus.Daemon.Application.Automations;

public sealed class AutomationExecutionContext
{
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly IActionHandlersProvider _handlersProvider;
    private readonly FactsDictionary _facts;
    private readonly IReadOnlyList<Integration> _integrations;
    private readonly IReadOnlyList<AutomationAction> _actions;
    private Task<FactsDictionary>? _currentTask;
    private Task? _mainTask;

    private AutomationId AutomationId { get; set; }

    public AutomationExecutionContext(
        IActionHandlersProvider actionHandlersProvider,
        Automation automation,
        IReadOnlyList<Integration> integrations,
        FactsDictionary facts)
    {
        _handlersProvider = actionHandlersProvider;
        _actions = automation.Actions;
        _integrations = integrations;
        _facts = facts;

        AutomationId = automation.Id;
    }

    public void Cancel()
    {
        _cancellationTokenSource.Cancel();
    }

    public void Run()
    {
        _mainTask = RunDetachedAsync();
    }

    private async Task RunDetachedAsync()
    {
        foreach (var action in _actions)
        {
            if (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                await RunActionAsync(action);
            }
        }
    }

    private async Task<bool> RunActionAsync(AutomationAction action)
    {
        var handler = _handlersProvider.GetHandlerTarget(action.Identifier);
        var parameters = GetHandlerParameters(handler.Method, action);

        var res = handler.Method.Invoke(handler.Target, parameters);

        _currentTask = res switch
        {
            Task<FactsDictionary> taskWithFacts => taskWithFacts,
            Task task => task.ContinueWith(_ => _facts, _cancellationTokenSource.Token),
            _ => null
        };

        if (_currentTask is null)
        {
            return false;
        }

        var facts = await _currentTask;
        return false;
    }

    private object[] GetHandlerParameters(MethodInfo method, AutomationAction action)
    {
        var parameters = method.GetParameters();
        var result = new object[parameters.Length];

        foreach (var parameter in parameters)
        {
            var fromParameterAttributeIdentifier = parameter.GetFromParameterIdentifier();
            var fromIntegrationAttribute = parameter.GetCustomAttribute<FromIntegrationsAttribute>();

            if (fromParameterAttributeIdentifier is not null)
            {
                result[parameter.Position] = GetParameterValue(fromParameterAttributeIdentifier, parameter.ParameterType, action);
            }
            else if (fromIntegrationAttribute is not null)
            {
                result[parameter.Position] = GetIntegrationValue(parameter.ParameterType);
            }
            else if (parameter.ParameterType.IsAssignableTo(typeof(CancellationToken)))
            {
                result[parameter.Position] = _cancellationTokenSource.Token;
            }
            else if (parameter.ParameterType.IsAssignableTo(typeof(AutomationId)))
            {
                result[parameter.Position] = AutomationId;
            }
            else
            {
                result[parameter.Position] = 0;
            }
        }
        return result;
    }
    
    private object GetParameterValue(string identifier, Type destType, AutomationAction action)
    {
        var parameter = action.Parameters.FirstOrDefault(p => p.Identifier == identifier);
        
        if (parameter is null)
        {
            throw new InvalidOperationException($"Parameter with identifier '{identifier}' not found");
        }

        try
        {
            if (parameter.Type != AutomationActionParameterType.Var)
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
            
            _facts.TryGetValue(parameter.Value, out var value);
            
            if (value is null)
            {
                throw new InvalidOperationException($"Unable to find value for parameter with identifier '{identifier}'");
            }
            return value switch
            {
                Fact<int> fact => fact.Value,
                Fact<string> fact => fact.Value,
                Fact<bool> fact => fact.Value,
                Fact<DateTime> fact => fact.Value,
                Fact<float> fact => fact.Value,
                Fact<object> fact => Convert.ChangeType(fact.Value, destType),
                _ => throw new InvalidOperationException($"Parameter with identifier '{identifier}' has invalid value")
            };
        } catch
        {
            throw new InvalidOperationException($"Parameter with identifier '{identifier}' has invalid value");
        }
    }
    
    private Integration GetIntegrationValue(Type destType)
    {
        var integration = _integrations.FirstOrDefault(i => i.GetType().IsAssignableTo(destType));
        
        if (integration is null)
        {
            throw new InvalidOperationException($"Integration of type '{destType.Name}' not found");
        }
        return integration;
    }
}
