namespace Zeus.Daemon.Infrastructure.Services.Providers.Notion.Contracts.Utils;

public record NotionPropertyContract(
    string Id,
    string Type,
    List<NotionTextContract>? Title);
