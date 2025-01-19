using Zeus.BuildingBlocks.Domain.Models;
using Zeus.Daemon.Domain.Providers.LeagueOfLegends.ValueObjects;

namespace Zeus.Daemon.Domain.Providers.LeagueOfLegends;

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
