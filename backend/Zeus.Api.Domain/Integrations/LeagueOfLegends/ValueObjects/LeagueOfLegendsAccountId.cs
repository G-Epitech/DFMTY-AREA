using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.LeagueOfLegends.ValueObjects;

public class LeagueOfLegendsAccountId : ValueObject
{
    public string Value { get; }
    
    public LeagueOfLegendsAccountId(string value)
    {
        Value = value;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
