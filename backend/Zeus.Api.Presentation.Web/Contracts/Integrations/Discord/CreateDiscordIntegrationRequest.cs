namespace Zeus.Api.Presentation.Web.Contracts.Integrations.Discord;

public record CreateDiscordIntegrationRequest(
    string Code,
    string State);
