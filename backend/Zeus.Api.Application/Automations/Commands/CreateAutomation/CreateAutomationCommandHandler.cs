using ErrorOr;

using Humanizer;

using MediatR;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Domain.Errors.Automations;
using Zeus.Common.Domain.AutomationAggregate;
using Zeus.Common.Domain.AutomationAggregate.Entities;
using Zeus.Common.Domain.AutomationAggregate.Enums;
using Zeus.Common.Domain.AutomationAggregate.ValueObjects;
using Zeus.Common.Domain.Integrations.Common.Enums;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Common.Domain.ProvidersSettings;

namespace Zeus.Api.Application.Automations.Commands.CreateAutomation;

public class CreateAutomationCommandHandler : IRequestHandler<CreateAutomationCommand, ErrorOr<Automation>>
{
    private readonly IAutomationWriteRepository _automationWriteRepository;
    private readonly IIntegrationReadRepository _integrationReadRepository;
    private readonly ProvidersSettings _providersSettings;

    internal class CreationContext
    {
        public required Dictionary<IntegrationId, IntegrationType> DependenciesTypes { get; init; }
        public required Dictionary<string, FactSchema> FactSchemas { get; init; }
    }

    private static class ErrorFactory
    {
        public static Error MissingSingleDependency(IHasDependenciesSchema source, string dependency) => source switch
        {
            ActionSchema _ => Errors.Automations.Action.MissingSingleDependency(dependency),
            TriggerSchema _ => Errors.Automations.Trigger.MissingSingleDependency(dependency),
            _ => throw new NotImplementedException()
        };

        public static Error MissingMultipleDependency(IHasDependenciesSchema source, string dependency) => source switch
        {
            ActionSchema _ => Errors.Automations.Action.MissingMultipleDependency(dependency),
            TriggerSchema _ => Errors.Automations.Trigger.MissingMultipleDependency(dependency),
            _ => throw new NotImplementedException()
        };

        public static Error TooManyDependencies(IHasDependenciesSchema source, string dependency) => source switch
        {
            ActionSchema _ => Errors.Automations.Action.TooManyDependencies(dependency),
            TriggerSchema _ => Errors.Automations.Trigger.TooManyDependencies(dependency),
            _ => throw new NotImplementedException()
        };

        public static Error NotFoundDependency(IHasDependenciesSchema source, IntegrationId dependency) => source switch
        {
            ActionSchema _ => Errors.Automations.Action.NotFoundDependency(dependency),
            TriggerSchema _ => Errors.Automations.Trigger.NotFoundDependency(dependency),
            _ => throw new NotImplementedException()
        };

        public static Error InvalidDependencyType(IHasDependenciesSchema source, IntegrationId dependency, IntegrationType type) => source switch
        {
            ActionSchema _ => Errors.Automations.Action.InvalidDependencyType(dependency, type),
            TriggerSchema _ => Errors.Automations.Trigger.InvalidDependencyType(dependency, type),
            _ => throw new NotImplementedException()
        };
    }

    public CreateAutomationCommandHandler(IAutomationWriteRepository automationWriteRepository, ProvidersSettings providersSettings,
        IIntegrationReadRepository integrationReadRepository)
    {
        _automationWriteRepository = automationWriteRepository;
        _providersSettings = providersSettings;
        _integrationReadRepository = integrationReadRepository;
    }

    public async Task<ErrorOr<Automation>> Handle(CreateAutomationCommand command, CancellationToken cancellationToken)
    {
        CreationContext ctx = new() { DependenciesTypes = await GetAutomationDependencies(command), FactSchemas = new Dictionary<string, FactSchema>() };

        var trigger = CreateTrigger(command.Trigger, ctx);
        var actions = CreateActions(command.Actions, ctx);

        if (trigger.IsError)
        {
            return trigger.Errors;
        }
        if (actions.IsError)
        {
            return actions.Errors;
        }

        var automation = Automation.Create(
            command.Label,
            command.Description,
            command.OwnerId,
            trigger.Value,
            actions.Value,
            command.Enabled
        );
        await _automationWriteRepository.AddAutomationAsync(automation, cancellationToken);
        return automation;
    }

    private ErrorOr<AutomationTrigger> CreateTrigger(CreateAutomationTriggerCommand command, CreationContext ctx)
    {
        #region Validate identifier

        (string? provider, string? trigger) = ProvidersSettings.ExplodeIdentifier(command.Identifier);
        var schema = provider is not null ? _providersSettings.GetProviderSchema(provider) : null;

        if (!_providersSettings.IsTriggerIdentifierValid($"{provider}.{trigger}") || schema is null || provider is null || trigger is null)
        {
            return Errors.Automations.Trigger.InvalidIdentifier;
        }

        #endregion

        #region Validate and create parameters

        var triggerSchema = schema.Triggers[trigger];
        var parameters = CreateTriggerParameters(command, triggerSchema);

        if (parameters.IsError)
        {
            return parameters.FirstError;
        }

        #endregion

        #region Validate dependencies

        var dependencies = GetDependencies(command.Dependencies, triggerSchema, ctx);

        if (dependencies.IsError)
        {
            return dependencies.FirstError;
        }

        #endregion

        #region Register facts

        foreach ((string? fact, FactSchema? factSchema) in triggerSchema.Facts)
        {
            ctx.FactSchemas[$"T.{fact}"] = factSchema;
        }

        #endregion

        return AutomationTrigger.Create(command.Identifier, parameters.Value, dependencies.Value);
    }

    private static ErrorOr<List<AutomationTriggerParameter>> CreateTriggerParameters(CreateAutomationTriggerCommand command, TriggerSchema schema)
    {
        var parameters = new List<AutomationTriggerParameter>();

        foreach (var parameter in command.Parameters)
        {
            var res = CreateTriggerParameter(parameter, schema);
            if (res.IsError)
            {
                return res.FirstError;
            }
            parameters.Add(res.Value);
        }
        return parameters;
    }

    private static ErrorOr<AutomationTriggerParameter> CreateTriggerParameter(CreateAutomationTriggerParameterCommand command, TriggerSchema schema)
    {
        var identifier = command.Identifier.Pascalize()!;

        if (!schema.Parameters.TryGetValue(identifier, out var parameterSchema))
        {
            return Errors.Automations.Trigger.InvalidParameterIdentifier;
        }

        if (!parameterSchema.IsValidValue(command.Value))
        {
            return Errors.Automations.Trigger.InvalidParameterValue;
        }

        return new AutomationTriggerParameter { Identifier = identifier, Value = command.Value, };
    }

    private ErrorOr<List<AutomationAction>> CreateActions(List<CreateAutomationActionCommand> commands, CreationContext ctx)
    {
        var actions = new List<AutomationAction>();
        var rank = 0;

        foreach (var command in commands)
        {
            var action = CreateAction(command, rank, ctx);
            if (action.IsError)
            {
                return action.FirstError;
            }

            actions.Add(action.Value);
            rank += 1;
        }
        return actions;
    }

    private ErrorOr<AutomationAction> CreateAction(CreateAutomationActionCommand command, int rank, CreationContext ctx)
    {
        #region Validate identifier

        (string? provider, string? action) = ProvidersSettings.ExplodeIdentifier(command.Identifier);
        var schema = provider is not null ? _providersSettings.GetProviderSchema(provider) : null;

        if (!_providersSettings.IsActionIdentifierValid($"{provider}.{action}") || schema is null || provider is null || action is null)
        {
            return Errors.Automations.Action.InvalidIdentifier;
        }

        #endregion

        #region Validate and create parameters

        var actionSchema = schema.Actions[action];
        var parameters = CreateActionParameters(command, actionSchema, ctx);

        if (parameters.IsError)
        {
            return parameters.FirstError;
        }

        #endregion

        #region Validate dependencies

        var dependencies = GetDependencies(command.Dependencies, actionSchema, ctx);

        if (dependencies.IsError)
        {
            return dependencies.FirstError;
        }

        #endregion

        #region Register facts

        foreach ((string? fact, FactSchema? factSchema) in actionSchema.Facts)
        {
            ctx.FactSchemas[$"{rank}.{fact}"] = factSchema;
        }

        #endregion

        return AutomationAction.Create(command.Identifier, rank, parameters.Value, dependencies.Value);
    }

    private static ErrorOr<List<AutomationActionParameter>> CreateActionParameters(CreateAutomationActionCommand command, ActionSchema schema, CreationContext ctx)
    {
        var parameters = new List<AutomationActionParameter>();
        foreach (var parameter in command.Parameters)
        {
            var res = CreateActionParameter(parameter, schema, ctx);
            if (res.IsError)
            {
                return res.FirstError;
            }
            parameters.Add(res.Value);
        }

        return parameters;
    }

    private static ErrorOr<AutomationActionParameter> CreateActionParameter(CreateAutomationActionParameterCommand command, ActionSchema schema, CreationContext ctx)
    {
        var identifier = command.Identifier.Pascalize()!;

        if (!schema.Parameters.TryGetValue(identifier, out var parameterSchema))
        {
            return Errors.Automations.Action.InvalidParameterIdentifier;
        }

        if (command.Type is AutomationActionParameterType.Var)
        {
            if (!ctx.FactSchemas.TryGetValue(command.Value, out var refSchema))
            {
                return Errors.Automations.Action.InvalidParameterReference(command.Value);
            }
            if (!parameterSchema.IsValidRef(refSchema))
            {
                return Errors.Automations.Action.InvalidParameterReferenceType(command.Value, refSchema.Type, parameterSchema.Type);
            }
            return new AutomationActionParameter { Identifier = identifier, Value = command.Value, Type = AutomationActionParameterType.Var };
        }


        if (!parameterSchema.IsValidValue(command.Value))
        {
            return Errors.Automations.Action.InvalidParameterValue(command.Value, parameterSchema.Type);
        }

        return new AutomationActionParameter { Identifier = identifier, Value = command.Value, Type = AutomationActionParameterType.Raw };
    }

    private Task<Dictionary<IntegrationId, IntegrationType>> GetAutomationDependencies(CreateAutomationCommand command)
    {
        var dependencies = command.Actions
            .SelectMany(a => a.Dependencies)
            .Union(command.Trigger.Dependencies)
            .Distinct()
            .Select(g => new IntegrationId(g))
            .ToList();

        return _integrationReadRepository.GetIntegrationTypesByIdsAsync(command.OwnerId, dependencies);
    }

    private static ErrorOr<List<IntegrationId>> GetDependencies(List<Guid> raw, IHasDependenciesSchema schema, CreationContext ctx)
    {
        var result = new List<IntegrationId>();
        var dependencies = raw.Distinct().Select(g => new IntegrationId(g)).ToList();
        var grouped = GroupDependenciesByType(dependencies, ctx);
        var notFound = dependencies.FirstOrDefault(d => !DependencyExists(d, ctx));

        if (notFound is not null)
        {
            return ErrorFactory.NotFoundDependency(schema, notFound);
        }

        foreach ((IntegrationType type, DependencyRuleSchema rule) in schema.Dependencies)
        {
            if (!grouped.TryGetValue(type, out var group) && !rule.Optional)
            {
                return rule is { Require: DependencyRequirements.Multiple }
                    ? ErrorFactory.MissingMultipleDependency(schema, type.ToString())
                    : ErrorFactory.MissingSingleDependency(schema, type.ToString());
            }

            switch (rule)
            {
                case { Optional: false, Require: DependencyRequirements.Multiple } when group is null || group.Count == 0:
                    return ErrorFactory.MissingMultipleDependency(schema, type.ToString());
                case { Optional: false, Require: DependencyRequirements.Single } when group is null || group.Count != 1:
                    return ErrorFactory.MissingSingleDependency(schema, type.ToString());
                case { Optional: true, Require: DependencyRequirements.Single } when group is not null && group.Count > 1:
                    return ErrorFactory.TooManyDependencies(schema, type.ToString());
            }

            if (group is not null)
            {
                result.AddRange(group);
            }
        }

        var unexpected = dependencies.Except(result).FirstOrDefault();

        if (unexpected is not null)
        {
            return ErrorFactory.InvalidDependencyType(schema, unexpected, ctx.DependenciesTypes[unexpected]);
        }

        return result;
    }

    private static Dictionary<IntegrationType, List<IntegrationId>> GroupDependenciesByType(List<IntegrationId> dependencies, CreationContext ctx)
    {
        var res = new Dictionary<IntegrationType, List<IntegrationId>>();

        foreach (var dependency in dependencies)
        {
            if (!ctx.DependenciesTypes.TryGetValue(dependency, out var withType))
            {
                continue;
            }
            if (res.TryGetValue(withType, out List<IntegrationId>? value))
            {
                value.Add(dependency);
            }
            else
            {
                res[withType] = [dependency];
            }
        }
        return res;
    }

    private static bool DependencyExists(IntegrationId dependency, CreationContext ctx)
    {
        return ctx.DependenciesTypes.ContainsKey(dependency);
    }
}
