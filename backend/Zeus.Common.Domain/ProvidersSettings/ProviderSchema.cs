namespace Zeus.Common.Domain.ProvidersSettings;

public class ProviderSchema
{
    public ProviderSchema(
        string name,
        string iconUri,
        string color,
        Dictionary<string, TriggerSchema> triggers,
        Dictionary<string, ActionSchema> actions
    )
    {
        Name = name;
        IconUri = iconUri;
        Color = color;
        Triggers = triggers;
        Actions = actions;
    }

    public string Name { get; }
    public string IconUri { get; }
    public string Color { get; }
    public Dictionary<string, TriggerSchema> Triggers { get; }
    public Dictionary<string, ActionSchema> Actions { get; }
}
