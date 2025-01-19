using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Api.Domain.Integrations.Github.ValueObjects;

public class GithubUserId : ValueObject
{
    public long Value { get; }

    public GithubUserId(long value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
