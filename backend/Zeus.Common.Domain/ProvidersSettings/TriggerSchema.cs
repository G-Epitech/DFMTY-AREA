using System.Diagnostics.CodeAnalysis;

using Zeus.Common.Domain.Integrations.Common.Enums;

namespace Zeus.Common.Domain.ProvidersSettings;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public class TriggerSchema
{
    public TriggerSchema(
        string name,
        string description,
        string icon,
        Dictionary<string, ParameterSchema> parameters,
        Dictionary<string, FactSchema> facts,
        Dictionary<IntegrationType, DependencyRuleSchema> dependencies
    )
    {
        Name = name;
        Description = description;
        Icon = icon;
        Parameters = parameters;
        Facts = facts;
        Dependencies = dependencies;
    }

    public string Name { get; }
    public string Description { get; }
    public string Icon { get; }
    public Dictionary<string, ParameterSchema> Parameters { get; }
    public Dictionary<string, FactSchema> Facts { get; }
    public Dictionary<IntegrationType, DependencyRuleSchema> Dependencies { get; }
}
