using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Daemon.Domain.LeagueOfLegends.ValueObjects;

public class LeagueOfLegendsMatchId : ValueObject
{
    public string Value { get; }

    public LeagueOfLegendsMatchId(string value)
    {
        Value = value;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
