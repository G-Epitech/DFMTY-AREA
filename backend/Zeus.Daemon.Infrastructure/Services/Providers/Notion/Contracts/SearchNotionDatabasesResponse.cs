namespace Zeus.Daemon.Infrastructure.Services.Providers.Notion.Contracts;

public record SearchNotionDatabasesResponse(
    string Object,
    string? NextCursor,
    bool HasMore,
    string Type,
    string RequestId,
    List<GetNotionDatabaseResponse> Results);
