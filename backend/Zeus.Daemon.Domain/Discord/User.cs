namespace Zeus.Daemon.Domain.Discord;

public record User(
    string Id,
    string Username,
    string Discriminator,
    bool? Bot);
