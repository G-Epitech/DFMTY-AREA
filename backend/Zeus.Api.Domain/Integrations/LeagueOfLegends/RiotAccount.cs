using Zeus.Api.Domain.Integrations.LeagueOfLegends.ValueObjects;
using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.LeagueOfLegends;

public class RiotAccount : Entity<RiotAccountId>
{
    public RiotAccount(RiotAccountId id, string gameName, string tagLine) : base(id)
    {
        GameName = gameName;
        TagLine = tagLine;
    }

    public string GameName { get; private set; }
    public string TagLine { get; private set; }
}
