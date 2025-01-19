namespace Zeus.Daemon.Infrastructure.Services.Providers.Github.Contracts;

public record GetGithubObjectUserResult(
    Int64 Id,
    string Login);
