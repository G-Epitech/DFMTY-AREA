using Zeus.Common.Domain.Models;

namespace Zeus.Api.Domain.AutomationAggregate.ValueObjects;

public sealed class AutomationId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private AutomationId(Guid value)
    {
        Value = value;
    }

    public static AutomationId CreateUnique()
    {
        return new AutomationId(Guid.NewGuid());
    }


    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618
    private AutomationId()
    {
    }
#pragma warning restore CS8618
}
