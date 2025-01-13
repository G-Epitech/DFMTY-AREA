namespace Zeus.Api.Presentation.Web.Contracts.Integrations.Notion;

public record GetNotionWorkspaceDatabaseResponse(
    string Id,
    string Title,
    string? Description,
    string? Icon,
    Uri Uri);
