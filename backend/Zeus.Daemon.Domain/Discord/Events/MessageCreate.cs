namespace Zeus.Daemon.Domain.Discord.Events;

public class MessageCreate : Message
{
    public string? GuildId { get; init; }

    public MessageCreate(string id,
        string channelId,
        User author,
        string content,
        string timestamp,
        string? guildId = null) : base(id, channelId, author, content, timestamp)
    {
        GuildId = guildId;
    }
}
