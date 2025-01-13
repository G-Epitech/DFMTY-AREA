namespace Zeus.Api.Infrastructure.Services.Integrations.Notion.Contracts.Utils;

public record NotionParentContract(
    string Type,
    string? PageId,
    string? DatabaseId,
    bool? Workspace);
