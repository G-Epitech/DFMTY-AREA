using Zeus.BuildingBlocks.Domain.Models;
using Zeus.Daemon.Domain.LeagueOfLegends.ValueObjects;

namespace Zeus.Daemon.Domain.LeagueOfLegends;

public class LeagueOfLegendsSummoner : Entity<LeagueOfLegendsSummonerId>
{
    public LeagueOfLegendsSummoner(LeagueOfLegendsSummonerId id, LeagueOfLegendsSummonerAccountId accountId,
        RiotAccountId riotAccountId, LeagueOfLegendsSummonerProfileIconId profileIconId, long revisionDate,
        long summonerLevel) : base(id)
    {
        AccountId = accountId;
        RiotAccountId = riotAccountId;
        ProfileIconId = profileIconId;
        RevisionDate = revisionDate;
        SummonerLevel = summonerLevel;
    }

    public LeagueOfLegendsSummonerAccountId AccountId { get; private set; }
    public RiotAccountId RiotAccountId { get; private set; }
    public LeagueOfLegendsSummonerProfileIconId ProfileIconId { get; private set; }
    public long RevisionDate { get; private set; }
    public long SummonerLevel { get; private set; }
}
