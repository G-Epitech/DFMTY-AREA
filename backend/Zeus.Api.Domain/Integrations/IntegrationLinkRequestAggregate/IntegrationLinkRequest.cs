using System.Diagnostics.CodeAnalysis;

using Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate.ValueObjects;
using Zeus.BuildingBlocks.Domain.Models;
using Zeus.Common.Domain.Integrations.Common.Enums;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.NonPublicConstructors)]
public class IntegrationLinkRequest : AggregateRoot<IntegrationLinkRequestId>
{
    private IntegrationLinkRequest(
        IntegrationLinkRequestId id,
        UserId ownerId,
        IntegrationType integrationType,
        DateTime updatedAt,
        DateTime createdAt)
        : base(id, updatedAt, createdAt)
    {
        OwnerId = ownerId;
        Type = integrationType;
    }

#pragma warning disable CS8618
    private IntegrationLinkRequest()
    {
    }
#pragma warning restore CS8618
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
        return new IntegrationLinkRequest(
            IntegrationLinkRequestId.CreateUnique(),
            ownerId,
            integrationType,
            DateTime.UtcNow,
            DateTime.UtcNow
        );
    }
}
