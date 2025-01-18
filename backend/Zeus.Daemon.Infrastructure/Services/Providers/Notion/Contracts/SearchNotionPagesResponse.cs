namespace Zeus.Daemon.Infrastructure.Services.Providers.Notion.Contracts;

public record SearchNotionPagesResponse(
    string Object,
    string? NextCursor,
    bool HasMore,
    string Type,
    string RequestId,
    List<GetNotionPageResponse> Results);
