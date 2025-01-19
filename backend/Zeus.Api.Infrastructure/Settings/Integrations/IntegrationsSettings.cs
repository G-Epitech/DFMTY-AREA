namespace Zeus.Api.Infrastructure.Settings.Integrations;

public class IntegrationsSettings
{
    public const string SectionName = nameof(IntegrationsSettings);

    public DiscordSettings Discord { get; init; } = null!;
    public NotionSettings Notion { get; init; } = null!;
    public OpenAiSettings OpenAi { get; init; } = null!;
    public RiotSettings Riot { get; init; } = null!;
    public GithubSettings Github { get; init; } = null!;
    public GmailSettings Gmail { get; init; } = null!;
}
