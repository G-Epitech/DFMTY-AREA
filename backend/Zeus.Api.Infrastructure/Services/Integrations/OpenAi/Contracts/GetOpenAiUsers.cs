namespace Zeus.Api.Infrastructure.Services.Integrations.OpenAi.Contracts;

public record GetOpenAiUsers(
    string Object,
    List<GetOpenAiUser> Data);

public record GetOpenAiUser(
    string Object,
    string Id,
    string Name,
    string Email,
    string Role,
    long AddedAt);
