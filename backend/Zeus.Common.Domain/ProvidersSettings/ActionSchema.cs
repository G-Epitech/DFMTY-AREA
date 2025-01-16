using Zeus.Common.Domain.Integrations.Common.Enums;

namespace Zeus.Common.Domain.ProvidersSettings;

public class ActionSchema
{
    public ActionSchema(
        string name,
        string description,
        string icon,
        Dictionary<string, ParameterSchema> parameters,
        Dictionary<string, FactSchema> facts,
        Dictionary<IntegrationType, IntegrationRuleSchema> integrations
    )
    {
        Name = name;
        Description = description;
        Icon = icon;
        Parameters = parameters;
        Facts = facts;
        Integrations = integrations;
    }

    public string Name { get; }
    public string Description { get; }
    public string Icon { get; }
    public Dictionary<string, ParameterSchema> Parameters { get; }
    public Dictionary<string, FactSchema> Facts { get; }
    public Dictionary<IntegrationType, IntegrationRuleSchema> Integrations { get; }
}
