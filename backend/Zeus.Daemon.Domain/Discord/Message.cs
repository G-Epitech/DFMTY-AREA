namespace Zeus.Daemon.Domain.Discord;

public class Message
{
    public Message(string id, string channelId, User author, string content, string timestamp)
    {
        Id = id;
        ChannelId = channelId;
        Author = author;
        Content = content;
        Timestamp = timestamp;
    }

    public string Id { get; init; }
    public string ChannelId { get; init; }
    public User Author { get; init; }
    public string Content { get; init; }
    public string Timestamp { get; init; }
}
