namespace Zeus.Daemon.Infrastructure.Settings.Integrations;

public class IntegrationsSettings
{
    public const string SectionName = nameof(IntegrationsSettings);

    public DiscordSettings Discord { get; init; } = null!;
    public NotionSettings Notion { get; init; } = null!;
}
