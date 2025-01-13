namespace Zeus.Api.Application.Integrations.Query.Notion.GetNotionWorkspaceDatabases;

public record GetNotionWorkspaceDatabaseQueryResult(
    string Id,
    string Title,
    string? Description,
    string? Icon,
    Uri Uri);
