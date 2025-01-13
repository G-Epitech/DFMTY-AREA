namespace Zeus.Api.Presentation.Web.Contracts.Integrations.OpenAi;

public record CreateOpenAiIntegrationRequest(
    string ApiToken,
    string AdminApiToken);
