using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.LeagueOfLegends.ValueObjects;

public class LeagueOfLegendsSummonerProfileIconId : ValueObject
{
    public LeagueOfLegendsSummonerProfileIconId(uint value)
    {
        Value = value;
    }

    public uint Value { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
