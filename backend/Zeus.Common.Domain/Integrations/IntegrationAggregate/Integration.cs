using Zeus.BuildingBlocks.Domain.Models;
using Zeus.Common.Domain.Integrations.Common.Enums;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Common.Domain.Integrations.IntegrationAggregate;

public abstract class Integration : AggregateRoot<IntegrationId>
{
    /// <summary>
    /// List of tokens associated with the integration.
    /// </summary>
    protected List<IntegrationToken> _tokens = [];

    protected Integration(
        IntegrationId id,
        IntegrationType type,
        UserId ownerId,
        string clientId,
        List<IntegrationToken> tokens,
        DateTime updatedAt,
        DateTime createdAt)
        : base(id, updatedAt, createdAt)
    {
        Type = type;
        OwnerId = ownerId;
        ClientId = clientId;
        _tokens = tokens;
    }

#pragma warning disable CS8618
    protected Integration()
    {
    }
#pragma warning restore CS8618

    /// <summary>
    /// Type of the integration.
    /// </summary>
    /// <example>
    /// IntegrationType type = integrationType.Google;
    /// </example>
    public IntegrationType Type { get; }

    /// <summary>
    /// Owner of the integration. In other words, the user that created the integration.
    /// </summary>
    public UserId OwnerId { get; private set; }

    /// <summary>
    /// Remote client id of the integration.
    /// </summary>
    public string ClientId { get; private set; }

    /// <summary>
    /// Public list of tokens associated with the integration.
    /// </summary>
    public IReadOnlyList<IntegrationToken> Tokens => _tokens.AsReadOnly();

    /// <summary>
    /// Determines if the integration is valid and has at least one token.
    /// </summary>
    public abstract bool IsValid { get; }

    /// <summary>
    /// Adds a token to the integration.
    /// </summary>
    /// <param name="token">
    /// Token to add to the integration.
    /// </param>
    public void AddToken(IntegrationToken token)
    {
        _tokens.Add(token);
    }

    /// <summary>
    /// Removes a token from the integration.
    /// </summary>
    /// <param name="token">
    /// Token to remove from the integration.
    /// </param>
    public void RemoveToken(IntegrationToken token)
    {
        _tokens.Remove(token);
    }

    public static Type? GetImplementationFromType(IntegrationType type)
    {
        return type switch
        {
            IntegrationType.Discord => typeof(DiscordIntegration),
            IntegrationType.Gmail => typeof(GmailIntegration),
            IntegrationType.Notion => typeof(NotionIntegration),
            IntegrationType.OpenAi => typeof(OpenAiIntegration),
            IntegrationType.LeagueOfLegends => typeof(LeagueOfLegendsIntegration),
            IntegrationType.Github => typeof(GithubIntegration),
            _ => null
        };
    }

    public static IntegrationType? GetTypeFromImplementation(Type type)
    {
        return type switch
        {
            not null when type == typeof(DiscordIntegration) => IntegrationType.Discord,
            not null when type == typeof(GmailIntegration) => IntegrationType.Gmail,
            not null when type == typeof(NotionIntegration) => IntegrationType.Notion,
            not null when type == typeof(OpenAiIntegration) => IntegrationType.OpenAi,
            not null when type == typeof(LeagueOfLegendsIntegration) => IntegrationType.LeagueOfLegends,
            not null when type == typeof(GithubIntegration) => IntegrationType.Github,
            _ => null
        };
    }
}
