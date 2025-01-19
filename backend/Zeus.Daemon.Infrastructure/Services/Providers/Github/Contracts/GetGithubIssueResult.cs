namespace Zeus.Daemon.Infrastructure.Services.Providers.Github.Contracts;

public record GetGithubIssueResult(
    string Url,
    Int64 Id,
    string HtmlUrl,
    int Number,
    string Title,
    GetGithubObjectUserResult User,
    string? Body);
