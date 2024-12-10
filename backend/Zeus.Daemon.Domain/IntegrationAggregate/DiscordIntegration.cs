using Zeus.Daemon.Domain.IntegrationAggregate.Enums;
using Zeus.Daemon.Domain.IntegrationAggregate.ValueObjects;
using Zeus.Daemon.Domain.User.ValueObjects;

namespace Zeus.Daemon.Domain.IntegrationAggregate;

public sealed class DiscordIntegration : Integration
{
    private DiscordIntegration(IntegrationId id, UserId ownerId, string clientId)
        : base(id, IntegrationType.Discord, ownerId, clientId)
    {
    }

    public override bool IsValid
    {
        get
        {
            return
                _tokens.Any(x => x.Usage == IntegrationTokenUsage.Refresh) &&
                _tokens.Any(x => x.Usage == IntegrationTokenUsage.Access);
        }
    }

    public static DiscordIntegration Create(UserId ownerId, string clientId)
    {
        return new DiscordIntegration(IntegrationId.CreateUnique(), ownerId, clientId);
    }
}
