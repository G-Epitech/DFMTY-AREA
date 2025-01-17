using System.Reflection;

using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.Entities;
using Zeus.Common.Domain.AutomationAggregate.Enums;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Daemon.Application.Attributes;
using Zeus.Daemon.Application.Extensions;
using Zeus.Daemon.Application.Interfaces.HandlerProviders;
using Zeus.Daemon.Domain.Automations;

namespace Zeus.Daemon.Application.Execution;

public sealed class AutomationExecutionContext
{
    private readonly IReadOnlyList<AutomationAction> _actions;
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly FactsDictionary _facts;
    private readonly IActionHandlersProvider _handlersProvider;
    private readonly IReadOnlyDictionary<IntegrationId, Integration> _integrations;
    private Task<FactsDictionary>? _currentTask;
    private Task? _mainTask;

    public AutomationExecutionContext(
        IActionHandlersProvider actionHandlersProvider,
        Automation automation,
        IReadOnlyDictionary<IntegrationId, Integration> integrations,
        FactsDictionary facts)
    {
        _handlersProvider = actionHandlersProvider;
        _actions = automation.Actions;
        _integrations = integrations;
        _facts = FillFactsFromTrigger(facts);

        AutomationId = automation.Id;
    }

    private AutomationId AutomationId { get; set; }

    public void Cancel()
    {
        _cancellationTokenSource.Cancel();
    }

    public void Run()
    {
        _mainTask = RunDetachedAsync();
    }

    private static FactsDictionary FillFactsFromTrigger(FactsDictionary triggerFacts)
    {
        var facts = new FactsDictionary();

        foreach (var fact in triggerFacts)
        {
            facts[$"T.{fact.Key}"] = fact.Value;
        }
        return facts;
    }

    private void AddActionFacts(AutomationAction action, FactsDictionary facts)
    {
        foreach (var fact in facts)
        {
            _facts[$"{action.Rank}.{fact.Key}"] = fact.Value;
        }
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
        var handler = _handlersProvider.GetHandler(action.Identifier);
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

        AddActionFacts(action, facts);
        _currentTask = null;
        return true;
    }

    private object?[] GetHandlerParameters(MethodInfo method, AutomationAction action)
    {
        var parameters = method.GetParameters();
        var result = new object?[parameters.Length];
        var dependencies = GetActionDependencies(action);

        foreach (var parameter in parameters)
        {
            var fromParametersAttributeIdentifier = parameter.GetParameterIdentifierFromAttribute();
            var fromIntegrationAttribute = parameter.GetCustomAttribute<FromIntegrationsAttribute>();

            if (fromParametersAttributeIdentifier is not null)
            {
                result[parameter.Position] = GetParameterValue(fromParametersAttributeIdentifier, parameter.ParameterType, action);
            }
            else if (fromIntegrationAttribute is not null)
            {
                result[parameter.Position] = StepUtils.GetFromIntegrationsValue(parameter, dependencies);
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
        }
        catch
        {
            throw new InvalidOperationException($"Parameter with identifier '{identifier}' has invalid value");
        }
    }

    private List<Integration> GetActionDependencies(AutomationAction step)
    {
        return step.Dependencies.Select(p => _integrations[p]).ToList();
    }
}
