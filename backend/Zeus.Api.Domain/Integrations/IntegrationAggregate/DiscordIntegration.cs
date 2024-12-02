using Zeus.Api.Domain.Integrations.Enums;
using Zeus.Api.Domain.Integrations.IntegrationAggregate.Enums;
using Zeus.Api.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Api.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Domain.Integrations.IntegrationAggregate;

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
                _tokens.Any(x => x.Usage == ServiceTokenUsage.Refresh) &&
                _tokens.Any(x => x.Usage == ServiceTokenUsage.Access);
        }
    }

    public static DiscordIntegration Create(UserId ownerId, string clientId)
    {
        return new DiscordIntegration(IntegrationId.CreateUnique(), ownerId, clientId);
    }
}
