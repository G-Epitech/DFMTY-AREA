using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Daemon.Domain.Providers.Github.ValueObjects;

public class GithubIssueId : ValueObject
{
    public GithubIssueId(long value)
    {
        Value = value;
    }

    public long Value { get; }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
