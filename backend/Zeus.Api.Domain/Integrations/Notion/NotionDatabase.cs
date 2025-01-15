using Zeus.Api.Domain.Integrations.Notion.ValueObjects;
using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.Notion;

public class NotionDatabase : Entity<NotionDatabaseId>
{
    public string? Icon { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public NotionUserId CreatedBy { get; private set; }
    public DateTime LastEditedAt { get; private set; }
    public NotionUserId LastEditedBy { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public bool IsInline { get; private set; }
    public NotionParent Parent { get; private set; }
    public Uri Uri { get; private set; }
    public bool Archived { get; private set; }
    public bool InTrash { get; private set; }

    public NotionDatabase(NotionDatabaseId id,
        string? icon,
        DateTime createdAt,
        NotionUserId createdBy,
        DateTime lastEditedAt,
        NotionUserId lastEditedBy,
        string title,
        bool isInline,
        NotionParent parent,
        Uri uri,
        bool archived,
        string? description,
        bool inTrash) : base(id)
    {
        Icon = icon;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        LastEditedAt = lastEditedAt;
        LastEditedBy = lastEditedBy;
        Title = title;
        Description = description;
        IsInline = isInline;
        Parent = parent;
        Uri = uri;
        Archived = archived;
        InTrash = inTrash;
    }
}
