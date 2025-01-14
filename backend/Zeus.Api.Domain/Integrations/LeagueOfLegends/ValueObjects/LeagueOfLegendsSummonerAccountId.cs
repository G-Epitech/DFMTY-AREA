using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.LeagueOfLegends.ValueObjects;

public class LeagueOfLegendsSummonerAccountId : ValueObject
{
    public string Value { get; }
    
    public LeagueOfLegendsSummonerAccountId(string value)
    {
        Value = value;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
