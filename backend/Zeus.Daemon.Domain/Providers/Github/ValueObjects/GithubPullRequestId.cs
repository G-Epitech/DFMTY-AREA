using Zeus.BuildingBlocks.Domain.Models;

namespace Zeus.Daemon.Domain.Providers.Github.ValueObjects;

public class GithubPullRequestId : ValueObject
{
    public GithubPullRequestId(long value)
    {
        Value = value;
    }

    public long Value { get; }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
