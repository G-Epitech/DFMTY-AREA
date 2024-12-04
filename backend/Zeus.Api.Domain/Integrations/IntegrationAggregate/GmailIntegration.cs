using Zeus.Api.Domain.Integrations.Enums;
using Zeus.Api.Domain.Integrations.IntegrationAggregate.Enums;
using Zeus.Api.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Api.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Domain.Integrations.IntegrationAggregate;

public sealed class GmailIntegration : Integration
{
    private GmailIntegration(IntegrationId id, UserId ownerId, string clientId)
        : base(id, IntegrationType.Gmail, ownerId, clientId)
    {
    }
    
#pragma warning disable CS8618
    private GmailIntegration()
    {
    }
#pragma warning restore CS8618

    public override bool IsValid
    {
        get
        {
            return
                _tokens.Any(x => x.Usage == ServiceTokenUsage.Refresh) &&
                _tokens.Any(x => x.Usage == ServiceTokenUsage.Access);
        }
    }

    public static GmailIntegration Create(UserId ownerId, string clientId)
    {
        return new GmailIntegration(IntegrationId.CreateUnique(), ownerId, clientId);
    }
}
