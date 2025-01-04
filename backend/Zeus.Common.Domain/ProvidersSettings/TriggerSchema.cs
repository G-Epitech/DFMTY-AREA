namespace Zeus.Common.Domain.ProvidersSettings;

public class TriggerSchema
{
    public string Name { get; }
    public string Description { get; }
    public string Icon { get; }
    public Dictionary<string, ParameterSchema> Parameters { get; }
    public Dictionary<string, FactSchema> Facts { get; }
    
    public TriggerSchema(
        string name,
        string description,
        string icon,
        Dictionary<string, ParameterSchema> parameters,
        Dictionary<string, FactSchema> facts
    )
    {
        Name = name;
        Description = description;
        Icon = icon;
        Parameters = parameters;
        Facts = facts;
    }
}
