using System.Diagnostics.CodeAnalysis;

using Zeus.Common.Domain.Integrations.Common.Enums;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.Enums;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Common.Domain.Integrations.IntegrationAggregate;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.NonPublicConstructors)]

public sealed class NotionIntegration : Integration
{
    public NotionIntegration(
        IntegrationId id,
        UserId ownerId,
        string clientId,
        DateTime updatedAt,
        DateTime createdAt)
        : base(id, IntegrationType.Notion, ownerId, clientId, updatedAt, createdAt)
    {
    }

#pragma warning disable CS8618
    private NotionIntegration()
    {
    }
#pragma warning restore CS8618

    public override bool IsValid
    {
        get
        {
            return
                _tokens.Any(x => x.Usage == IntegrationTokenUsage.Access);
        }
    }

    public static NotionIntegration Create(UserId ownerId, string clientId)
    {
        return new NotionIntegration(
            IntegrationId.CreateUnique(),
            ownerId,
            clientId,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }
}
