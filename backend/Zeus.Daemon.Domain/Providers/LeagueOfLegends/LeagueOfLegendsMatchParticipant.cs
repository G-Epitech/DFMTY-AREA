using Zeus.BuildingBlocks.Domain.Models;
using Zeus.Daemon.Domain.Providers.LeagueOfLegends.ValueObjects;

namespace Zeus.Daemon.Domain.Providers.LeagueOfLegends;

public class LeagueOfLegendsMatchParticipant : Entity<RiotAccountId>
{
    public bool Win { get; private set; }
    public string ChampionName { get; private set; }
    public float Kda { get; private set; }

    public LeagueOfLegendsMatchParticipant(RiotAccountId id, bool win, string championName, float kda) :
        base(id)
    {
        Win = win;
        ChampionName = championName;
        Kda = kda;
    }
}
