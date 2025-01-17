using ErrorOr;

using Zeus.Common.Domain.Common.Enums;
using Zeus.Common.Domain.Integrations.Common.Enums;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;

namespace Zeus.Api.Domain.Errors.Automations;

public static partial class Errors
{
    public static class Automations
    {
        public static Error NotFound => Error.NotFound(
            code: "Automations.NotFound",
            description: "Automation not found."
        );

        public static class Trigger
        {
            public static Error InvalidIdentifier => Error.Validation(
                code: "Automations.Trigger.InvalidIdentifier",
                description: "Invalid trigger identifier."
            );

            public static Error InvalidParameterIdentifier => Error.Validation(
                code: "Automations.Trigger.InvalidParameterIdentifier",
                description: "Invalid trigger parameter identifier."
            );

            public static Error InvalidParameterValue => Error.Validation(
                code: "Automations.Trigger.InvalidParameterValue",
                description: "Invalid trigger parameter value."
            );

            public static Error MissingSingleDependency(string dependency) => Error.Validation(
                code: "Automations.Trigger.MissingSingleDependency",
                description: $"Missing single dependency: {dependency}."
            );

            public static Error MissingMultipleDependency(string dependency) => Error.Validation(
                code: "Automations.Trigger.MissingMultipleDependency",
                description: $"Missing multiple dependency: {dependency}."
            );

            public static Error TooManyDependencies(string dependency) => Error.Validation(
                code: "Automations.Trigger.TooManyDependencies",
                description: $"Too many dependencies: {dependency}."
            );

            public static Error NotFoundDependency(IntegrationId dependencyId) => Error.Validation(
                code: "Automations.Trigger.NotFoundDependency",
                description: $"Not found dependency: {dependencyId}."
            );

            public static Error InvalidDependencyType(IntegrationId dependencyId, IntegrationType type) => Error.Validation(
                code: "Automations.Trigger.InvalidDependencyType",
                description: $"Invalid dependency type: {type.ToString()} ({dependencyId})."
            );
        }

        public static class Action
        {
            public static Error InvalidIdentifier => Error.Validation(
                code: "Automations.Action.InvalidIdentifier",
                description: "Invalid action identifier."
            );

            public static Error InvalidParameterIdentifier => Error.Validation(
                code: "Automations.Action.InvalidParameterIdentifier",
                description: "Invalid action parameter identifier."
            );

            public static Error InvalidParameterReference(string value) => Error.Validation(
                code: "Automations.Action.InvalidParameterReference",
                description: $"Invalid variable reference: {value}. Maybe not valid at this point."
            );

            public static Error InvalidParameterReferenceType(string reference, VariableType current, VariableType expected) => Error.Validation(
                code: "Automations.Action.InvalidParameterReferenceType",
                description: $"Invalid variable reference type: {reference}. Current: {current}. Expected: {expected}."
            );

            public static Error InvalidParameterValue(string value, VariableType expected) => Error.Validation(
                code: "Automations.Action.InvalidParameterValue",
                description: $"Invalid value '{value}' for type {expected}."
            );

            public static Error MissingSingleDependency(string dependency) => Error.Validation(
                code: "Automations.Action.MissingSingleDependency",
                description: $"Missing single dependency: {dependency}."
            );

            public static Error MissingMultipleDependency(string dependency) => Error.Validation(
                code: "Automations.Action.MissingMultipleDependency",
                description: $"Missing multiple dependency: {dependency}."
            );

            public static Error TooManyDependencies(string dependency) => Error.Validation(
                code: "Automations.Action.TooManyDependencies",
                description: $"Too many dependencies: {dependency}."
            );

            public static Error NotFoundDependency(IntegrationId dependencyId) => Error.Validation(
                code: "Automations.Action.NotFoundDependency",
                description: $"Not found dependency: {dependencyId}."
            );

            public static Error InvalidDependencyType(IntegrationId dependencyId, IntegrationType type) => Error.Validation(
                code: "Automations.Action.InvalidDependencyType",
                description: $"Invalid dependency type: {type.ToString()} ({dependencyId})."
            );
        }
    }
}
