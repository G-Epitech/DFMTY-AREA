using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Daemon.Domain.Providers.LeagueOfLegends.ValueObjects;

public class RiotAccountId : ValueObject
{
    public RiotAccountId(string value)
    {
        Value = value;
    }

    public string Value { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
