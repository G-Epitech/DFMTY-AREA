using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Daemon.Domain.Providers.Notion.ValueObjects;

public enum NotionParentType
{
    Workspace,
    Page,
    Database
}

public abstract class NotionParent : ValueObject
{
    public abstract NotionParentType Type { get; }
}

public class NotionParentWorkspace : NotionParent
{
    public override NotionParentType Type => NotionParentType.Workspace;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield break;
    }
}

public class NotionParentPage : NotionParent
{
    public NotionParentPage(NotionPageId id)
    {
        Id = id;
    }

    public override NotionParentType Type => NotionParentType.Page;

    public NotionPageId Id { get; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Id;
    }
}

public class NotionParentDatabase : NotionParent
{
    public NotionParentDatabase(NotionDatabaseId id)
    {
        Id = id;
    }

    public override NotionParentType Type => NotionParentType.Database;

    public NotionDatabaseId Id { get; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Id;
    }
}
