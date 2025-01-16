using Zeus.Common.Domain.Integrations.Common.Enums;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Common.Domain.ProvidersSettings;

namespace Zeus.Daemon.Application.Utils;

public static class HandlersValidationUtils
{
    public static void CheckFromIntegrationsParameterConformity(
        ParameterUtils.FromIntegrationsParameterInfo infos,
        string handlerType,
        string handlerIdentifier,
        IntegrationType matchingType,
        Dictionary<IntegrationType, IntegrationRuleSchema> schema
    )
    {
        var rule = schema[matchingType];
        var isSchemaCollection = rule.Require is IntegrationRequirements.Multiple or IntegrationRequirements.OneOrMore;

        switch (infos.IsCollection)
        {
            case true when !isSchemaCollection:
                throw new InvalidOperationException(
                    $"Invalid collection of type '{infos.Type.Name}' in {handlerType} '{handlerIdentifier}'. Parameter must not be a collection to receive {matchingType} integration. Allowed types are:\n{GetAllowedFromIntegrationsFormat(schema)}");
            case false when isSchemaCollection:
                throw new InvalidOperationException(
                    $"Invalid type '{infos.Type.Name}' in {handlerType} '{handlerIdentifier}''. Parameter must be a collection to receive {matchingType} integration. Allowed types are:\n{GetAllowedFromIntegrationsFormat(schema)}");
        }
        if (!infos.Optional && rule.Optional)
        {
            throw new InvalidOperationException(
                $"Invalid type '{infos.Type.Name}' in {handlerType} '{handlerIdentifier}''. Parameter must be marked as nullable to receive {matchingType} integration. Allowed types are:\n{GetAllowedFromIntegrationsFormat(schema)}");
        }
    }

    public static string GetAllowedFromIntegrationsFormat(Dictionary<IntegrationType, IntegrationRuleSchema> schema)
    {
        var final = string.Empty;

        foreach ((IntegrationType type, IntegrationRuleSchema rule) in schema)
        {
            var impl = Integration.GetImplementationFromType(type)!;
            if (rule.Require is IntegrationRequirements.Multiple or IntegrationRequirements.OneOrMore)
            {
                final += $"\tIReadOnlyList<{impl.Name}>\n\tIList<{impl.Name}>\n";
            }
            else if (rule.Optional)
            {
                final += $"\t{impl.Name}?\n";
            }
            else
            {
                final += $"\t{impl.Name}\n";
            }
        }
        return final;
    }
}
