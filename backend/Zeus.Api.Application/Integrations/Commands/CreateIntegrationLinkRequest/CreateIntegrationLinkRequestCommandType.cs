using Zeus.Api.Domain.Integrations.Common.Enums;

namespace Zeus.Api.Application.Integrations.Commands.CreateIntegrationLinkRequest;

public enum CreateIntegrationLinkRequestCommandType
{
    Discord,
    Gmail,
}

public static class CreateIntegrationLinkRequestCommandTypeExtensions
{
    public static IntegrationType ToIntegrationType(this CreateIntegrationLinkRequestCommandType type) =>
        type switch
        {
            CreateIntegrationLinkRequestCommandType.Discord => IntegrationType.Discord,
            CreateIntegrationLinkRequestCommandType.Gmail => IntegrationType.Gmail,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
}
