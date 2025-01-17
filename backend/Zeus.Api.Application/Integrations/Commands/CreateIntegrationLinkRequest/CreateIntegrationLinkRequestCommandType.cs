using Zeus.Common.Domain.Integrations.Common.Enums;

namespace Zeus.Api.Application.Integrations.Commands.CreateIntegrationLinkRequest;

public enum CreateIntegrationLinkRequestCommandType
{
    Discord,
    Gmail,
    Notion,
    OpenAi,
    LeagueOfLegends
}

public static class CreateIntegrationLinkRequestCommandTypeExtensions
{
    public static IntegrationType ToIntegrationType(this CreateIntegrationLinkRequestCommandType type) =>
        type switch
        {
            CreateIntegrationLinkRequestCommandType.Discord => IntegrationType.Discord,
            CreateIntegrationLinkRequestCommandType.Gmail => IntegrationType.Gmail,
            CreateIntegrationLinkRequestCommandType.Notion => IntegrationType.Notion,
            CreateIntegrationLinkRequestCommandType.OpenAi => IntegrationType.OpenAi,
            CreateIntegrationLinkRequestCommandType.LeagueOfLegends => IntegrationType.LeagueOfLegends,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
}
