using System.Reflection;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Zeus.Common.Domain.Common.Enums;
using Zeus.Common.Domain.ProvidersSettings;
using Zeus.Daemon.Application.Attributes;
using Zeus.Daemon.Application.Execution;
using Zeus.Daemon.Application.Extensions;
using Zeus.Daemon.Application.Interfaces.HandlerProviders;

namespace Zeus.Daemon.Application.Services.HandlerProviders;

public sealed class TriggerHandlersProvider : ITriggerHandlersProvider
{
    private static readonly Assembly Assembly = typeof(TriggerHandlersProvider).Assembly;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger _logger;
    private readonly Dictionary<string, TriggerHandler> _handlers = new();

    public TriggerHandlersProvider(
        IServiceProvider serviceProvider,
        ProvidersSettings providersSettings,
        ILogger<TriggerHandlersProvider> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;

        var handlerTypes = Assembly.GetTriggerHandlersTypes();

        foreach (var type in handlerTypes)
        {
            CheckHandlerDeclarationsAndRegister(type, providersSettings);
        }
        EnsureEveryTriggerHasHandler(providersSettings);
        _logger.LogDebug("{count} trigger handlers have been registered", _handlers.Count);
    }

    private void RegisterHandler(string triggerFullIdentifier, Type hostingClass, MethodInfo onRegisterMethod, MethodInfo onRemoveMethod)
    {
        _handlers[triggerFullIdentifier] = new TriggerHandler
        {
            OnRegisterMethod = onRegisterMethod, OnRemoveMethod = onRemoveMethod, Target = _serviceProvider.GetRequiredService(hostingClass)
        };
    }

    private void EnsureEveryTriggerHasHandler(ProvidersSettings providersSettings)
    {
        foreach (var triggerIdentifier in providersSettings.AllTriggerIdentifiers)
        {
            if (!_handlers.ContainsKey(triggerIdentifier))
            {
                throw new InvalidOperationException($"Trigger '{triggerIdentifier}' has no handler registered");
            }
        }
    }

    public TriggerHandler GetHandler(string triggerIdentifier)
    {
        if (_handlers.TryGetValue(triggerIdentifier, out var triggerHandlerTarget))
        {
            return triggerHandlerTarget;
        }
        throw new InvalidOperationException($"Trigger handler with identifier '{triggerIdentifier}' not found");
    }

    private void CheckHandlerDeclarationsAndRegister(Type hostingClass, ProvidersSettings providersSettings)
    {
        var triggerFullIdentifier = hostingClass.GetCustomAttribute<TriggerHandlerAttribute>()?.Identifier;

        if (string.IsNullOrEmpty(triggerFullIdentifier))
        {
            throw new InvalidOperationException($"Type '{hostingClass.Name}' has no valid TriggerHandlerAttribute");
        }

        var triggerSchema = GetTriggerSchema(triggerFullIdentifier, providersSettings);
        var onRegisterMethod = GetOnRegisterMethod(hostingClass, triggerFullIdentifier, triggerSchema);
        var onRemoveMethod = GetOnRemoveMethod(hostingClass, triggerFullIdentifier);

        RegisterHandler(triggerFullIdentifier, hostingClass, onRegisterMethod, onRemoveMethod);
    }

    private static MethodInfo GetOnRegisterMethod(Type hostingClass, string triggerFullIdentifier, TriggerSchema triggerSchema)
    {
        var method = hostingClass.GetMethods().FirstOrDefault(m => m.HasAttribute<OnTriggerRegisterAttribute>());

        if (method is null)
        {
            throw new InvalidOperationException($"Type '{hostingClass.Name}' has no valid OnTriggerRegisterAttribute");
        }
        var parameters = method.GetParameters().ToList();

        CheckOnRegisterMethodParameters(parameters, triggerFullIdentifier, triggerSchema);
        CheckReturnType<Task<bool>>(method);
        return method;
    }

    private static void CheckOnRegisterMethodParameters(List<ParameterInfo> parameters, string triggerFullIdentifier, TriggerSchema triggerSchema)
    {
        foreach (var parameter in parameters)
        {
            if (!parameter.IsValidOnTriggerRegisterMethodParameter())
            {
                throw new InvalidOperationException(
                    $"Parameter '{parameter.Name}' is not a valid trigger register method parameter, in trigger '{triggerFullIdentifier}'");
            }

            if (parameter.HasAttribute<FromParametersAttribute>())
            {
                CheckOnRegisterMethodTypedParameter(parameter, triggerFullIdentifier, triggerSchema);
            }
        }
    }


    private static void CheckOnRegisterMethodTypedParameter(ParameterInfo parameter, string triggerFullIdentifier, TriggerSchema triggerSchema)
    {
        var identifier = parameter.GetParameterIdentifierFromAttribute();

        if (identifier is null || !triggerSchema.Parameters.TryGetValue(identifier, out var parameterSchema))
        {
            throw new InvalidOperationException($"Parameter '{identifier}' not found in trigger '{triggerFullIdentifier}'");
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

    private static MethodInfo GetOnRemoveMethod(Type hostingClass, string triggerFullIdentifier)
    {
        var method = hostingClass.GetMethods().FirstOrDefault(m => m.HasAttribute<OnTriggerRemoveAttribute>());

        if (method is null)
        {
            throw new InvalidOperationException($"Type '{hostingClass.Name}' has no valid OnTriggerRemoveAttribute");
        }
        var parameters = method.GetParameters().ToList();

        CheckOnRemoveParameters(parameters, triggerFullIdentifier);
        CheckReturnType<Task<bool>>(method);
        return method;
    }

    private static void CheckOnRemoveParameters(List<ParameterInfo> parameters, string triggerFullIdentifier)
    {
        foreach (var parameter in parameters)
        {
            if (!parameter.IsValidOnTriggerRemoveMethodParameter())
            {
                throw new InvalidOperationException($"Parameter '{parameter.Name}' is not a valid trigger remove method parameter, in trigger '{triggerFullIdentifier}'");
            }
        }
    }

    private static TriggerSchema GetTriggerSchema(string triggerFullIdentifier, ProvidersSettings providersSettings)
    {
        var provider = triggerFullIdentifier.Split('.').FirstOrDefault();
        var action = triggerFullIdentifier.Split('.').LastOrDefault();

        if (string.IsNullOrEmpty(provider) || string.IsNullOrEmpty(action))
        {
            throw new InvalidOperationException($"Trigger '{triggerFullIdentifier}' has invalid format");
        }

        var providerSchema = providersSettings.GetProviderSchema(provider);

        if (providerSchema is null)
        {
            throw new InvalidOperationException($"Provider '{provider}' not found in ProvidersSettings");
        }

        if (!providerSchema.Triggers.TryGetValue(action, out TriggerSchema? schema))
        {
            throw new InvalidOperationException($"Trigger '{action}' not found in provider '{provider}'");
        }
        return schema;
    }

    private static void CheckReturnType<T>(MethodInfo method)
    {
        if (method.ReturnType != typeof(T))
        {
            throw new InvalidOperationException($"Method '{method.Name}' has invalid return type on '{method.DeclaringType?.Name}'");
        }
    }
}
