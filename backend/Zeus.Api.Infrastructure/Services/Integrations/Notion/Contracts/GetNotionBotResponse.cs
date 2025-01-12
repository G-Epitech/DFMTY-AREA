namespace Zeus.Api.Infrastructure.Services.Integrations.Notion.Contracts;

public record GetNotionBotResponse(
    string Object,
    string Id,
    string Name,
    string AvatarUrl,
    string Type,
    string RequestId,
    GetNotionBotInfosResponse Bot);

public record GetNotionBotInfosResponse(
    string WorkspaceName,
    GetNotionBotOwnerResponse Owner);

public record GetNotionBotOwnerResponse(
    string Type,
    GetNotionBotOwnerUserResponse User);

public record GetNotionBotOwnerUserResponse(
    string Object,
    string Id,
    string Name,
    string AvatarUrl,
    string Type,
    GetNotionBotOwnerUserPersonResponse Person);

public record GetNotionBotOwnerUserPersonResponse(
    string Email
);
