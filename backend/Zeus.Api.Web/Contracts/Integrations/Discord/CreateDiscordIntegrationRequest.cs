namespace Zeus.Api.Web.Contracts.Integrations.Discord;

public record CreateDiscordIntegrationRequest(
    string Code,
    string State);
