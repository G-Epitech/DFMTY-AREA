﻿using System.Diagnostics.CodeAnalysis;

using Zeus.Common.Domain.Integrations.Common.Enums;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.Enums;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Common.Domain.Integrations.IntegrationAggregate;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.NonPublicConstructors)]

public sealed class DiscordIntegration : Integration
{
    public DiscordIntegration(
        IntegrationId id,
        UserId ownerId,
        string clientId,
        List<IntegrationToken> tokens,
        DateTime updatedAt,
        DateTime createdAt)
        : base(id, IntegrationType.Discord, ownerId, clientId, tokens, updatedAt, createdAt)
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
            [],
            DateTime.UtcNow,
            DateTime.UtcNow);
    }
}
