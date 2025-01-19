namespace Zeus.Daemon.Infrastructure.Services.Providers.Gmail.Contracts;

public record GetGmailMessageResponse(
    string InternalDate,
    string HistoryId,
    string Id,
    string ThreadId,
    GetGmailMessagePayloadResponse Payload
);

public record GetGmailMessagePayloadResponse(
    GetGmailMessageHeaderResponse[] Headers,
    GetGmailMessagePartResponse[] Parts
);

public record GetGmailMessageHeaderResponse(
    string Name,
    string Value);

public record GetGmailMessagePartResponse(
    GetGmailMessageBodyResponse Body,
    string MimeType,
    string PartId);

public record GetGmailMessageBodyResponse(
    string? Data,
    int Size);
