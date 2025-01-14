namespace Zeus.Api.Presentation.Web.Contracts.Integrations.Notion;

public record CreateNotionIntegrationRequest(
    string Code,
    string State);
