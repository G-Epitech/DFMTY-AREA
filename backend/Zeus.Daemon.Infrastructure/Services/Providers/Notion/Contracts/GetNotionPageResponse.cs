using Zeus.Daemon.Infrastructure.Services.Providers.Notion.Contracts.Utils;

namespace Zeus.Daemon.Infrastructure.Services.Providers.Notion.Contracts;

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
