using Zeus.Api.Domain.Integrations.Common.Enums;
using Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate.ValueObjects;
using Zeus.Api.Domain.UserAggregate.ValueObjects;
using Zeus.Common.Domain.Models;

namespace Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate;

public class IntegrationLinkRequest : AggregateRoot<IntegrationLinkRequestId, Guid>
{
    /// <summary>
    /// The user that owns the integration link request.
    /// </summary>
    public UserId OwnerId { get; private set; }

    /// <summary>
    /// The type of the future integration.
    /// </summary>
    public IntegrationType Type { get; private set; }

    public static IntegrationLinkRequest Create(UserId ownerId, IntegrationType integrationType)
    {
        return new IntegrationLinkRequest(IntegrationLinkRequestId.CreateUnique(), ownerId, integrationType);
    }

    private IntegrationLinkRequest(IntegrationLinkRequestId id, UserId ownerId,
        IntegrationType integrationType) : base(id)
    {
        OwnerId = ownerId;
        Type = integrationType;
    }
}
