using Zeus.BuildingBlocks.Domain.Models;
using Zeus.Common.Domain.Authentication.Common;

namespace Zeus.Daemon.Domain.Providers.Notion.ValueObjects;

public class NotionWorkspaceTokens : ValueObject
{
    public NotionWorkspaceTokens(AccessToken accessToken, NotionWorkspaceId workspaceId,
        string botId)
    {
        AccessToken = accessToken;
        WorkspaceId = workspaceId;
        BotId = botId;
    }

    public AccessToken AccessToken { get; }
    public NotionWorkspaceId WorkspaceId { get; }
    public string BotId { get; }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return AccessToken;
        yield return WorkspaceId;
        yield return BotId;
    }
}
