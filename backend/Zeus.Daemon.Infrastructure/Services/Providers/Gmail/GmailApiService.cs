using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web;

using ErrorOr;

using Zeus.Common.Domain.Authentication.Common;
using Zeus.Daemon.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Daemon.Application.Providers.Gmail.Services;
using Zeus.Daemon.Application.Providers.Gmail.Services.GmailApiFilters;
using Zeus.Daemon.Domain.Errors.Services;
using Zeus.Daemon.Domain.Providers.Gmail.Entities;
using Zeus.Daemon.Domain.Providers.Gmail.ValueObjects;
using Zeus.Daemon.Infrastructure.Services.Providers.Gmail.Contracts;

namespace Zeus.Daemon.Infrastructure.Services.Providers.Gmail;

public partial class GmailApiService : IGmailApiService
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly HttpClient _httpClient;
    private readonly Uri _messagesApiEndpoint;

    public GmailApiService(IIntegrationsSettingsProvider settingsProvider)
    {
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

        _messagesApiEndpoint = new Uri(settingsProvider.Gmail.MessagesApiEndpoint);
    }

    public async Task<ErrorOr<List<GmailMessageId>>> GetMessagesAsync(AccessToken accessToken, GmailUserId userId, GetGmailMessagesFilters? filters = null,
        CancellationToken cancellationToken = default)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);

        if (filters is not null)
        {
            query.Add("q", filters.ToGmailQuery());
        }

        var uri = new Uri(_messagesApiEndpoint, $"users/{userId.Value}/messages?{query}");

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Value);

        HttpResponseMessage response = await _httpClient.GetAsync(uri, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return Errors.Services.Gmail.FailureDuringRequest;
        }

        var responseContent = await response.Content.ReadFromJsonAsync<GetGmailMessagesIdsResponse>(_jsonSerializerOptions, cancellationToken);

        if (responseContent is null)
        {
            return Errors.Services.Gmail.InvalidBody;
        }

        return responseContent.Messages?.Select(m => new GmailMessageId(m.Id)).ToList() ?? [];
    }

    public async Task<ErrorOr<GmailMessage>> GetMessageAsync(
        AccessToken accessToken,
        GmailMessageId messageId,
        GmailUserId userId,
        CancellationToken cancellationToken = default)

    {
        var uri = new Uri(_messagesApiEndpoint, $"users/{userId.Value}/messages/{messageId.Value}");

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Value);

        HttpResponseMessage response = await _httpClient.GetAsync(uri, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return Errors.Services.Gmail.FailureDuringRequest;
        }

        var responseContent = await response.Content.ReadFromJsonAsync<GetGmailMessageResponse>(_jsonSerializerOptions, cancellationToken);

        if (responseContent is null)
        {
            return Errors.Services.Gmail.InvalidBody;
        }

        var body = ExtractBody(responseContent);
        var metadata = ExtractMetadata(responseContent);

        return new GmailMessage(
            messageId,
            new GmailThreadId(responseContent.ThreadId),
            metadata.From,
            metadata.Author,
            metadata.To,
            metadata.Subject,
            body, metadata.ReceivedAt
        );
    }

    private static string ExtractBody(GetGmailMessageResponse response)
    {
        var part = response.Payload.Parts.FirstOrDefault(p => p.MimeType == "text/plain") ??
                   response.Payload.Parts.FirstOrDefault(p => p.MimeType == "text/html") ??
                   response.Payload.Parts.FirstOrDefault(p => p.MimeType == "text");

        switch (part)
        {
            case null when response.Payload.Parts.Length > 0:
                part = response.Payload.Parts[0];
                break;
            case null:
                return string.Empty;
        }

        var base64Data = part.Body.Data ?? string.Empty;
        return Encoding.UTF8.GetString(Convert.FromBase64String(base64Data));
    }

    private static GmailMessageMetadata ExtractMetadata(GetGmailMessageResponse response)
    {
        var fromHeader = response.Payload.Headers.FirstOrDefault(h => h.Name == "From")?.Value;
        var to = response.Payload.Headers.FirstOrDefault(h => h.Name == "To")?.Value;
        var subject = response.Payload.Headers.FirstOrDefault(h => h.Name == "Subject")?.Value;


        var from = fromHeader is null ? "No data" : ExtractPeopleFromHeader(fromHeader);
        var author = fromHeader is null ? from : ExtractAuthorFromHeader(fromHeader);
        to = to is null ? "No data" : ExtractPeopleFromHeader(to);
        subject ??= "No data";

        var receivedAt = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(response.InternalDate)).DateTime;

        return new GmailMessageMetadata
        {
            From = from,
            Author = author,
            To = to,
            Subject = subject,
            ReceivedAt = receivedAt
        };
    }

    private static string ExtractPeopleFromHeader(string header)
    {
        var match = EmailHeaderRegex().Match(header);
        return match.Success ? match.Groups["email"].Value : header;
    }

    private static string ExtractAuthorFromHeader(string header)
    {
        var match = EmailHeaderRegex().Match(header);
        return match.Success ? match.Groups["author"].Value : header;
    }

    private struct GmailMessageMetadata
    {
        public string From { get; init; }
        public string Author { get; init; }
        public string To { get; init; }
        public string Subject { get; init; }
        public DateTime ReceivedAt { get; init; }
    }

    [GeneratedRegex(@"(?<author>.*?)\s*<(?<email>.*?)>")]
    private static partial Regex EmailHeaderRegex();
}
