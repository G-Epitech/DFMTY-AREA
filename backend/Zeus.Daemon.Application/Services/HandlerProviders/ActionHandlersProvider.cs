using System.Reflection;

using Humanizer;

using Microsoft.Extensions.DependencyInjection;

using Zeus.Common.Domain.Common.Enums;
using Zeus.Common.Domain.ProvidersSettings;
using Zeus.Daemon.Application.Attributes;
using Zeus.Daemon.Application.Extensions;
using Zeus.Daemon.Application.Interfaces.HandlerProviders;
using Zeus.Daemon.Domain.Automations;

namespace Zeus.Daemon.Application.Services.HandlerProviders;

public class ActionHandlersProvider : IActionHandlersProvider
{
    private struct ActionHandlerDefinition
    {
        public Type HostingClass { get; set; }
        public MethodInfo Method { get; set; }
    }

    private static readonly Assembly Assembly = typeof(ActionHandlersProvider).Assembly;
    private readonly IServiceProvider _serviceProvider;

    private readonly Dictionary<string, ActionHandlerDefinition> _handlers = new();

    public ActionHandlersProvider(IServiceProvider serviceProvider, ProvidersSettings providersSettings)
    {
        _serviceProvider = serviceProvider;

        var typesWithHandlers = Assembly.GetActionHandlersHostingTypes();

        foreach (var type in typesWithHandlers)
        {
            CheckHandlerDeclarationsAndRegister(type, providersSettings);
        }
        EnsureEveryActionHasHandler(providersSettings);
    }

    private void RegisterHandler(Type hostingClass, MethodInfo method)
    {
        var actionFullIdentifier = method.GetCustomAttribute<ActionHandlerAttribute>()?.Identifier;

        if (string.IsNullOrEmpty(actionFullIdentifier))
        {
            throw new InvalidOperationException($"Method '{method.Name}' has no valid ActionHandlerAttribute");
        }

        _handlers[actionFullIdentifier] = new ActionHandlerDefinition
        {
            HostingClass = hostingClass,
            Method = method
        };
    }


    private void EnsureEveryActionHasHandler(ProvidersSettings providersSettings)
    {
        foreach (var actionIdentifier in providersSettings.AllActionIdentifiers)
        {
            if (!_handlers.ContainsKey(actionIdentifier))
            {
                throw new InvalidOperationException($"Action '{actionIdentifier}' has no handler registered");
            }
        }
    }

    public ActionHandler GetHandlerTarget(string actionIdentifier)
    {
        if (_handlers.TryGetValue(actionIdentifier, out var handlerDefinition))
        {
            return new ActionHandler
            {
                Method = handlerDefinition.Method,
                Target = _serviceProvider.GetRequiredService(handlerDefinition.HostingClass)
            };
            
        }
        throw new InvalidOperationException($"action handler with identifier '{actionIdentifier}' not found");
    }

    private void CheckHandlerDeclarationsAndRegister(Type hostingClass, ProvidersSettings providersSettings)
    {
        var methods = hostingClass
            .GetMethods()
            .Where(m => m.IsActionHandlerMethod())
            .ToList();

        foreach (var method in methods)
        {
            CheckHandlerDeclaration(method, providersSettings);
            RegisterHandler(hostingClass, method);
        }
    }

    private static void CheckHandlerDeclaration(MethodInfo method, ProvidersSettings providersSettings)
    {
        var actionFullIdentifier = method.GetCustomAttribute<ActionHandlerAttribute>()?.Identifier;
        var parameters = method
            .GetParameters()
            .Where(p => p.IsValidActionHandlerParameter())
            .ToList();
        
        if (string.IsNullOrEmpty(actionFullIdentifier))
        {
            throw new InvalidOperationException($"Method '{method.Name}' has no valid ActionHandlerAttribute");
        }

        var actionSchema = GetActionSchema(actionFullIdentifier, providersSettings);
        CheckParameters(parameters, actionFullIdentifier, actionSchema);
        CheckReturnType(method);
    }
    
    private static ActionSchema GetActionSchema(string actionFullIdentifier, ProvidersSettings providersSettings)
    {
        var provider = actionFullIdentifier.Split('.').FirstOrDefault();
        var action = actionFullIdentifier.Split('.').LastOrDefault();
        
        if (string.IsNullOrEmpty(provider) || string.IsNullOrEmpty(action))
        {
            throw new InvalidOperationException($"Action '{actionFullIdentifier}' has invalid format");
        }

        var providerSchema = providersSettings.GetProviderSchema(provider);

        if (providerSchema is null)
        {
            throw new InvalidOperationException($"Provider '{provider}' not found in ProvidersSettings");
        }

        if (!providerSchema.Actions.TryGetValue(action, out ActionSchema? schema))
        {
            throw new InvalidOperationException($"Action '{action}' not found in provider '{provider}'");
        }
        return schema;
    }

    private static void CheckParameters(List<ParameterInfo> parameters, string actionFullIdentifier, ActionSchema actionSchema)
    {
        foreach (var parameter in parameters)
        {
            if (parameter.HasFromParameterAttribute())
            {
                CheckTypedParameter(parameter, actionFullIdentifier, actionSchema);
            }
        }
    }
    
    private static void CheckTypedParameter(ParameterInfo parameter, string actionFullIdentifier, ActionSchema actionSchema)
    {
        var identifier = parameter.GetFromParameterIdentifier();
        
        if (identifier is null || !actionSchema.Parameters.TryGetValue(identifier, out var parameterSchema))
        {
            throw new InvalidOperationException($"Parameter '{identifier}' not found in action '{actionFullIdentifier}'");
        }

        bool isValid = parameterSchema.Type switch
        {
            VariableType.String => parameter.ParameterType.IsAssignableTo(typeof(string)),
            VariableType.Boolean => parameter.ParameterType.IsAssignableTo(typeof(bool)),
            VariableType.Integer => parameter.ParameterType.IsAssignableTo(typeof(int)),
            VariableType.Float => parameter.ParameterType.IsAssignableTo(typeof(float)),
            VariableType.Object => parameter.ParameterType.IsAssignableTo(typeof(object)),
            VariableType.Datetime => parameter.ParameterType.IsAssignableTo(typeof(DateTime)),
            _ => throw new InvalidOperationException($"Parameter '{parameter.Name}' has invalid type '{parameterSchema.Type}'")
        };

        if (!isValid)
        {
            throw new InvalidOperationException($"Parameter '{parameter.Name}' is not assignable to type '{parameterSchema.Type}'");
        }
    }
    
    private static void CheckReturnType(MethodInfo method)
    {
        if (method.ReturnType != typeof(Task<FactsDictionary>))
        {
            throw new InvalidOperationException($"Method '{method.Name}' has invalid return type");
        }
    }
}
