namespace Zeus.Common.Extensions.Queryable;

public record struct PageQuery {
    public int Index { get; init; }
    public int Limit { get; init; }
}
