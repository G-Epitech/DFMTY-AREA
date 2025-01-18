namespace Zeus.Daemon.Domain.Providers.Discord;

public record User(
    string Id,
    string Username,
    string Discriminator,
    bool? Bot);
