using Zeus.Common.Domain.Models;

namespace Zeus.Api.Domain.UserAggregate.ValueObjects;

public sealed class UserId : ValueObject
{
    public Guid Value { get; }

    public UserId(Guid value)
    {
        Value = value;
    }

    public static UserId CreateUnique()
    {
        return new UserId(Guid.NewGuid());
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618
    private UserId()
    {
    }
#pragma warning restore CS8618
}
