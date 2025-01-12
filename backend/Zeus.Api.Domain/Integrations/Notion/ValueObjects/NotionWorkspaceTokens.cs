using Zeus.BuildingBlocks.Domain.Models;
using Zeus.Common.Domain.Authentication.Common;

namespace Zeus.Api.Domain.Integrations.Notion.ValueObjects;

public class NotionWorkspaceTokens : ValueObject
{
    public AccessToken AccessToken { get; }
    public NotionWorkspaceId WorkspaceId { get; }
    public string BotId { get; }

    public NotionWorkspaceTokens(AccessToken accessToken, NotionWorkspaceId workspaceId,
        string botId)
    {
        AccessToken = accessToken;
        WorkspaceId = workspaceId;
        BotId = botId;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return AccessToken;
        yield return WorkspaceId;
        yield return BotId;
    }
}
