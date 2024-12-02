using Zeus.Api.Domain.Authentication.ValueObjects;
using Zeus.Api.Domain.Common.Services.Enums;
using Zeus.Api.Domain.ServiceAggregate.Enums;
using Zeus.Api.Domain.ServiceAggregate.ValueObjects;
using Zeus.Api.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Domain.ServiceAggregate;

public sealed class GmailService : Service
{
    private GmailService(ServiceId id, UserId ownerId, string clientId)
        : base(ServiceType.Gmail, id, ownerId, clientId)
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

    public static GmailService Create(UserId ownerId, string clientId)
    {
        return new GmailService(ServiceId.CreateUnique(), ownerId, clientId);
    }
}
