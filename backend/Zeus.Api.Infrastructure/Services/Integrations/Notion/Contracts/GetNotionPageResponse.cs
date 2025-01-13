using Zeus.Api.Infrastructure.Services.Integrations.Notion.Contracts.Utils;

namespace Zeus.Api.Infrastructure.Services.Integrations.Notion.Contracts;

public record GetNotionPageResponse(
    string Object,
    string Id,
    NotionIconContract? Icon,
    string CreatedTime,
    NotionObjectIdContract CreatedBy,
    NotionObjectIdContract LastEditedBy,
    Dictionary<string, NotionPropertyContract> Properties,
    string LastEditedTime,
    NotionParentContract Parent,
    string Url,
    bool Archived,
    bool InTrash);
