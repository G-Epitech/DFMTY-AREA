using Zeus.Api.Domain.Integrations.LeagueOfLegends.ValueObjects;
using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.LeagueOfLegends;

public class LeagueOfLegendsSummoner : Entity<LeagueOfLegendsSummonerId>
{
    public LeagueOfLegendsSummonerAccountId AccountId { get; private set; }
    public RiotAccountId RiotAccountId { get; private set; }
    public LeagueOfLegendsSummonerProfileIconId ProfileIconId { get; private set; }
    public long RevisionDate { get; private set; }
    public long SummonerLevel { get; private set; }

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
}
