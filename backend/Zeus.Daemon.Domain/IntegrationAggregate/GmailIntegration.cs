using Zeus.Daemon.Domain.IntegrationAggregate.Enums;
using Zeus.Daemon.Domain.IntegrationAggregate.ValueObjects;
using Zeus.Daemon.Domain.User.ValueObjects;

namespace Zeus.Daemon.Domain.IntegrationAggregate;

public sealed class GmailIntegration : Integration
{
    private GmailIntegration(IntegrationId id, UserId ownerId, string clientId)
        : base(id, IntegrationType.Gmail, ownerId, clientId)
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

    public static GmailIntegration Create(UserId ownerId, string clientId)
    {
        return new GmailIntegration(IntegrationId.CreateUnique(), ownerId, clientId);
    }
}
