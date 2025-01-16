using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.LeagueOfLegends.ValueObjects;

public class LeagueOfLegendsSummonerAccountId : ValueObject
{
    public LeagueOfLegendsSummonerAccountId(string value)
    {
        Value = value;
    }

    public string Value { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
