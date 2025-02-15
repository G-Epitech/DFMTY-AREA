namespace Zeus.Daemon.Domain.Providers.Discord.Events;

public class MessageCreate : Message
{
    public MessageCreate(string id,
        string channelId,
        User author,
        string content,
        string timestamp,
        string? guildId = null) : base(id, channelId, author, content, timestamp)
    {
        GuildId = guildId;
    }

    public string? GuildId { get; init; }
}
