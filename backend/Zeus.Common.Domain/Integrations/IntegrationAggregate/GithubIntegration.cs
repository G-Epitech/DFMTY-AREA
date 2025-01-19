using System.Diagnostics.CodeAnalysis;

using Zeus.Common.Domain.Integrations.Common.Enums;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.Enums;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Common.Domain.Integrations.IntegrationAggregate;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.NonPublicConstructors)]
public sealed class GithubIntegration : Integration
{
    public GithubIntegration(
        IntegrationId id,
        UserId ownerId,
        string clientId,
        List<IntegrationToken> tokens,
        DateTime updatedAt,
        DateTime createdAt)
        : base(id, IntegrationType.Github, ownerId, clientId, tokens, updatedAt, createdAt)
    {
    }

#pragma warning disable CS8618
    private GithubIntegration()
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

    public static GithubIntegration Create(UserId ownerId, string clientId)
    {
        return new GithubIntegration(
            IntegrationId.CreateUnique(),
            ownerId,
            clientId,
            [],
            DateTime.UtcNow,
            DateTime.UtcNow);
    }
}
