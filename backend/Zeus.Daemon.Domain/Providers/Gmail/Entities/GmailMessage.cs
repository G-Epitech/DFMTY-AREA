using Zeus.BuildingBlocks.Domain.Models;
using Zeus.Daemon.Domain.Providers.Gmail.ValueObjects;

namespace Zeus.Daemon.Domain.Providers.Gmail.Entities;

public sealed class GmailMessage : Entity<GmailMessageId>
{
    public GmailThreadId ThreadId { get; }
    public string Author { get; }
    public string From { get; }
    public string To { get; }
    public string Subject { get; }
    public string Body { get; }
    public DateTime ReceivedAt { get; }

    public GmailMessage(
        GmailMessageId id,
        GmailThreadId threadId,
        string from,
        string author,
        string to,
        string subject,
        string body,
        DateTime receivedAt)
        : base(id)
    {
        From = from;
        To = to;
        Subject = subject;
        Body = body;
        ReceivedAt = receivedAt;
        ThreadId = threadId;
        Author = author;
    }
}
