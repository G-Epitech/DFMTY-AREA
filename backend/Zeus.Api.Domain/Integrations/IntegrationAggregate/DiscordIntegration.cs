using Zeus.Api.Domain.Integrations.Common.Enums;
using Zeus.Api.Domain.Integrations.IntegrationAggregate.Enums;
using Zeus.Api.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Api.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Domain.Integrations.IntegrationAggregate;

public sealed class DiscordIntegration : Integration
{
    private DiscordIntegration(
        IntegrationId id,
        UserId ownerId,
        string clientId,
        DateTime updatedAt,
        DateTime createdAt)
        : base(id, IntegrationType.Discord, ownerId, clientId, updatedAt, createdAt)
    {
    }

#pragma warning disable CS8618
    private DiscordIntegration()
    {
    }
#pragma warning restore CS8618

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
        return new DiscordIntegration(
            IntegrationId.CreateUnique(),
            ownerId,
            clientId,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }
}
