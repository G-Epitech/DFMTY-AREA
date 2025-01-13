namespace Zeus.Api.Infrastructure.Services.Integrations.Notion.Contracts;

public record SearchNotionPagesResponse(
    string Object,
    string? NextCursor,
    bool HasMore,
    string Type,
    string RequestId,
    List<GetNotionPageResponse> Results);
