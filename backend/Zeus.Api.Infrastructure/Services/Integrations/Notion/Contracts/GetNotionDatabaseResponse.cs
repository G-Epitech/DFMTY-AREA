using Zeus.Api.Infrastructure.Services.Integrations.Notion.Contracts.Utils;

namespace Zeus.Api.Infrastructure.Services.Integrations.Notion.Contracts;

public record GetNotionDatabaseResponse(
    string Object,
    string Id,
    NotionIconContract? Icon,
    string CreatedTime,
    NotionObjectIdContract CreatedBy,
    NotionObjectIdContract LastEditedBy,
    string LastEditedTime,
    List<NotionTextContract> Title,
    List<NotionTextContract> Description,
    bool IsInline,
    NotionParentContract Parent,
    string Url,
    bool Archived,
    bool InTrash);
