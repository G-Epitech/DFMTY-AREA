using Zeus.BuildingBlocks.Domain.Models;
using Zeus.Daemon.Domain.Providers.Notion.ValueObjects;

namespace Zeus.Daemon.Domain.Providers.Notion;

public class NotionBot : Entity<NotionIntegrationId>
{
    private NotionBot(NotionIntegrationId id, string name, string workspaceName, NotionUser owner) : base(id)
    {
        Name = name;
        WorkspaceName = workspaceName;
        Owner = owner;
    }

    public string Name { get; private set; }
    public string WorkspaceName { get; private set; }
    public NotionUser Owner { get; private set; }

    public static NotionBot Create(NotionIntegrationId id, string name, string workspaceName, NotionUser owner)
    {
        return new NotionBot(id, name, workspaceName, owner);
    }
}
