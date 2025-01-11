using Zeus.Api.Domain.Integrations.Notion.ValueObjects;
using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.Notion;

public class NotionBot : Entity<NotionIntegrationId>
{
    public string Name { get; private set; }
    public string WorkspaceName { get; private set; }
    public NotionUser Owner { get; private set; }

    private NotionBot(NotionIntegrationId id, string name, string workspaceName, NotionUser owner) : base(id)
    {
        Name = name;
        WorkspaceName = workspaceName;
        Owner = owner;
    }

    public static NotionBot Create(NotionIntegrationId id, string name, string workspaceName, NotionUser owner)
    {
        return new NotionBot(id, name, workspaceName, owner);
    }
}