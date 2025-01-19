namespace Zeus.Api.Infrastructure.Services.Integrations.Notion.Contracts.Utils;

public record NotionTextContract(
    string Type,
    NotionTextContentContract Text,
    NotionTextAnnotationsContract Annotations,
    string PlainText,
    string? Href);

public record NotionTextContentContract(
    string Content);

public record NotionTextAnnotationsContract(
    bool Bold,
    bool Italic,
    bool Strikethrough,
    bool Underline,
    bool Code,
    string Color);
