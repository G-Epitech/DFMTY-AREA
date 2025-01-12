using System.Text.Json.Serialization;

namespace Zeus.Common.Domain.ProvidersSettings;

public class ProvidersSettings
{
    [JsonIgnore]
    private List<string>? _cachedActionsIdentifiers;

    [JsonIgnore]
    private List<string>? _cachedTriggersIdentifiers;

    [JsonIgnore]
    public IReadOnlyList<string> AllTriggerIdentifiers => _cachedTriggersIdentifiers ??= CacheTriggersIdentifiers();

    [JsonIgnore]
    public IReadOnlyList<string> AllActionIdentifiers => _cachedActionsIdentifiers ??= CacheActionsIdentifiers();

    public required ProviderSchema Discord { get; set; }

    private List<string> CacheTriggersIdentifiers()
    {
        _cachedTriggersIdentifiers = [];
        _cachedTriggersIdentifiers.AddRange(Discord.Triggers.Keys.Select(k => $"{nameof(Discord)}.{k}"));
        return _cachedTriggersIdentifiers;
    }

    private List<string> CacheActionsIdentifiers()
    {
        _cachedActionsIdentifiers = [];
        _cachedActionsIdentifiers.AddRange(Discord.Actions.Keys.Select(k => $"{nameof(Discord)}.{k}"));
        return _cachedActionsIdentifiers;
    }

    public ProviderSchema GetProviderSchema(string providerName)
    {
        return providerName switch
        {
            nameof(Discord) => Discord,
            _ => throw new InvalidOperationException($"Provider '{providerName}' not found")
        };
    }
}
