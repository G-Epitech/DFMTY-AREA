namespace Zeus.Daemon.Infrastructure.Services.Providers.Notion.Contracts.Utils;

public record NotionParentContract(
    string Type,
    string? PageId,
    string? DatabaseId,
    bool? Workspace);
