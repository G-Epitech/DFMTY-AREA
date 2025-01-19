namespace Zeus.Daemon.Application.Providers.Gmail.Services.GmailApiFilters;

public record GetGmailMessagesFilters
{
    public DateTime? After { get; set; }

    public string ToGmailQuery()
    {
        var query = String.Empty;

        if (After.HasValue)
        {
            query += $"after:{new DateTimeOffset(After.Value).ToUnixTimeSeconds()}";
        }

        return query;
    }

    public GetGmailMessagesFilters Copy() => new GetGmailMessagesFilters { After = After };
}
