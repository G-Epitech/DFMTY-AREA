namespace Zeus.Api.Infrastructure.Services.Integrations.Notion.Contracts;

public record NotionOAuth2TokenResponse(
    string AccessToken,
    string TokenType,
    string BotId,
    string WorkspaceIcon,
    string WorkspaceId,
    string WorkspaceName);
