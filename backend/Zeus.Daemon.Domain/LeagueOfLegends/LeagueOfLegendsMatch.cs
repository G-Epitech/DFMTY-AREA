using Zeus.BuildingBlocks.Domain.Models;
using Zeus.Daemon.Domain.LeagueOfLegends.ValueObjects;

namespace Zeus.Daemon.Domain.LeagueOfLegends;

public class LeagueOfLegendsMatch : Entity<LeagueOfLegendsMatchId>
{
    public string MatchType { get; private set; }
    public long MatchDuration { get; private set; }

    public List<LeagueOfLegendsMatchParticipant> Participants { get; private set; }

    public LeagueOfLegendsMatch(LeagueOfLegendsMatchId id, string matchType, long matchDuration,
        List<LeagueOfLegendsMatchParticipant> participants) :
        base(id)
    {
        MatchType = matchType;
        MatchDuration = matchDuration;
        Participants = participants;
    }
}
