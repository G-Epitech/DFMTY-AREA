using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

using Humanizer;

namespace Zeus.Common.Domain.ProvidersSettings;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public class ProvidersSettings
{
    [JsonIgnore] private List<string>? _cachedActionsIdentifiers;

    [JsonIgnore] private List<string>? _cachedTriggersIdentifiers;

    [JsonIgnore] public IReadOnlyList<string> AllTriggerIdentifiers => _cachedTriggersIdentifiers ??= CacheTriggersIdentifiers();

    [JsonIgnore] public IReadOnlyList<string> AllActionIdentifiers => _cachedActionsIdentifiers ??= CacheActionsIdentifiers();

    public required ProviderSchema Discord { get; set; }
    public required ProviderSchema Notion { get; set; }
    public required ProviderSchema OpenAi { get; set; }

    private List<string> CacheTriggersIdentifiers()
    {
        _cachedTriggersIdentifiers = [];
        _cachedTriggersIdentifiers.AddRange(Discord.Triggers.Keys.Select(k => $"{nameof(Discord)}.{k}"));
        _cachedTriggersIdentifiers.AddRange(Notion.Triggers.Keys.Select(k => $"{nameof(Notion)}.{k}"));
        _cachedTriggersIdentifiers.AddRange(Notion.Triggers.Keys.Select(k => $"{nameof(OpenAi)}.{k}"));
        return _cachedTriggersIdentifiers;
    }

    private List<string> CacheActionsIdentifiers()
    {
        _cachedActionsIdentifiers = [];
        _cachedActionsIdentifiers.AddRange(Discord.Actions.Keys.Select(k => $"{nameof(Discord)}.{k}"));
        _cachedActionsIdentifiers.AddRange(Notion.Actions.Keys.Select(k => $"{nameof(Notion)}.{k}"));
        _cachedActionsIdentifiers.AddRange(Notion.Actions.Keys.Select(k => $"{nameof(OpenAi)}.{k}"));
        return _cachedActionsIdentifiers;
    }

    public ProviderSchema GetProviderSchema(string providerName)
    {
        return providerName switch
        {
            nameof(Discord) => Discord,
            nameof(Notion) => Notion,
            nameof(OpenAi) => OpenAi,
            _ => throw new InvalidOperationException($"Provider '{providerName}' not found")
        };
    }

    public ProviderSchema? GetProviderSchemaFromIdentifier(string identifier)
    {
        var providerName = identifier.Split('.').FirstOrDefault();

        return providerName is not null ? GetProviderSchema(providerName) : null;
    }

    public static (string?, string?) ExplodeIdentifier(string identifier)
    {
        var parts = identifier.Split('.');

        return parts.Length == 2 ? (parts[0].Pascalize(), parts[1].Pascalize()) : (null, null);
    }

    public bool IsTriggerIdentifierValid(string identifier)
    {
        return AllTriggerIdentifiers.Contains(identifier);
    }

    public bool IsActionIdentifierValid(string identifier)
    {
        return AllActionIdentifiers.Contains(identifier);
    }
}
