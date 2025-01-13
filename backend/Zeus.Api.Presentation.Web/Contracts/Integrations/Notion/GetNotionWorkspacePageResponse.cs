namespace Zeus.Api.Presentation.Web.Contracts.Integrations.Notion;

public record GetNotionWorkspacePageResponse(
    string Id,
    string Title,
    string? Icon,
    Uri Uri);
