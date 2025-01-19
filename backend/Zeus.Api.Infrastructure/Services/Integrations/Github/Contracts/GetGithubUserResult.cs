namespace Zeus.Api.Infrastructure.Services.Integrations.Github.Contracts;

public record GetGithubUserResult(
    string Login,
    Int64 Id,
    string AvatarUrl,
    string Url,
    string Type,
    string Name,
    string? Company,
    string? Blog,
    string? Location,
    string? Email,
    string? Bio,
    int Followers,
    int Following);
