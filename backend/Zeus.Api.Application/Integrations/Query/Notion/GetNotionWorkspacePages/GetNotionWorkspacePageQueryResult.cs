namespace Zeus.Api.Application.Integrations.Query.Notion.GetNotionWorkspacePages;

public record GetNotionWorkspacePageQueryResult(
    string Id,
    string Title,
    string? Icon,
    Uri Uri);
