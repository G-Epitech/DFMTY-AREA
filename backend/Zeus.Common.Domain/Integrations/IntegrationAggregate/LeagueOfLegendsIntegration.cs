using Zeus.Common.Domain.Integrations.Common.Enums;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Common.Domain.Integrations.IntegrationAggregate;

public sealed class LeagueOfLegendsIntegration : Integration
{
    public LeagueOfLegendsIntegration(
        IntegrationId id,
        UserId ownerId,
        string clientId,
        DateTime updatedAt,
        DateTime createdAt)
        : base(id, IntegrationType.LeagueOfLegends, ownerId, clientId, updatedAt, createdAt)
    {
    }

#pragma warning disable CS8618
    private LeagueOfLegendsIntegration()
    {
    }
#pragma warning restore CS8618

    public override bool IsValid
    {
        get
        {
            return true;
        }
    }

    public static LeagueOfLegendsIntegration Create(UserId ownerId, string clientId)
    {
        return new LeagueOfLegendsIntegration(
            IntegrationId.CreateUnique(),
            ownerId,
            clientId,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }
}
