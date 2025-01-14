using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.LeagueOfLegends.ValueObjects;

public class LeagueOfLegendsSummonerProfileIconId : ValueObject
{
    public uint Value { get; }

    public LeagueOfLegendsSummonerProfileIconId(uint value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
