namespace Zeus.Api.Presentation.Web.Contracts.Integrations.Gmail;

public record CreateGmailIntegrationRequest(
    string Code,
    string State);
