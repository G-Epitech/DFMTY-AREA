namespace Zeus.Daemon.Infrastructure.Integrations;

public class IntegrationsSettings
{
    public const string SectionName = nameof(IntegrationsSettings);
    
    public DiscordSettings Discord { get; init; } = null!;
}
