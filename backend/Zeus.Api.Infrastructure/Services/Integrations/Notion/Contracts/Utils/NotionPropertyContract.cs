namespace Zeus.Api.Infrastructure.Services.Integrations.Notion.Contracts.Utils;

public record NotionPropertyContract(
    string Id,
    string Type,
    List<NotionTextContract>? Title);
