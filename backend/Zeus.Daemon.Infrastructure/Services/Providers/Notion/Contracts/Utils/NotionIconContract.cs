namespace Zeus.Daemon.Infrastructure.Services.Providers.Notion.Contracts.Utils;

public record NotionIconContract(
    string Type,
    string? Emoji,
    NotionExternalIconContract? External);

public record NotionExternalIconContract(
    string Url);
