namespace Zeus.Api.Presentation.Web.Contracts.Integrations.Github;

public record CreateGithubIntegrationRequest(
    string Code,
    string State);
