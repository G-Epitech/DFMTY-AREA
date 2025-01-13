namespace Zeus.Daemon.Infrastructure.Settings.MessageBrokers;

public class RabbitMqSettings
{
    public static string SectionName => $"{nameof(MessageBrokers)}:RabbitMq";
    public string Host { get; init; } = null!;
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
}
