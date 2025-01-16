using Zeus.Api.Domain.Integrations.Notion.ValueObjects;
using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.Notion;

public class NotionPage : Entity<NotionPageId>
{
    public NotionPage(NotionPageId id,
        string? icon,
        DateTime createdAt,
        NotionUserId createdBy,
        DateTime lastEditedAt,
        NotionUserId lastEditedBy,
        string title,
        string? description,
        NotionParent parent,
        Uri uri,
        bool archived,
        bool inTrash) : base(id)
    {
        Icon = icon;
        CreatedAt = createdAt;
        CreatedBy = createdBy;
        LastEditedAt = lastEditedAt;
        LastEditedBy = lastEditedBy;
        Title = title;
        Description = description;
        Parent = parent;
        Uri = uri;
        Archived = archived;
        InTrash = inTrash;
    }

    public string? Icon { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public NotionUserId CreatedBy { get; private set; }
    public DateTime LastEditedAt { get; private set; }
    public NotionUserId LastEditedBy { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public NotionParent Parent { get; private set; }
    public Uri Uri { get; private set; }
    public bool Archived { get; private set; }
    public bool InTrash { get; private set; }
}
