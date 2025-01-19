namespace Zeus.Daemon.Infrastructure.Services.Providers.Gmail.Contracts;

public record GetGmailMessageIdResponse(
    string Id,
    string ThreadId
);

public record GetGmailMessagesIdsResponse(
    GetGmailMessageIdResponse[]? Messages
);
