using System.Buffers.Text;
using System.Text;
using System.Web;

using ErrorOr;

using MediatR;

using Zeus.Api.Application.Integrations.Commands.CreateIntegrationLinkRequest;
using Zeus.Api.Application.Interfaces.Services.Settings.Integrations;

namespace Zeus.Api.Application.Integrations.Commands.GenerateDiscordOauth2Uri;

public class GenerateDiscordOauth2UriCommandHandler : IRequestHandler<GenerateDiscordOauth2UriCommand,
    ErrorOr<GenerateDiscordOauth2UriCommandResult>>
{
    private readonly ISender _sender;
    private readonly IIntegrationsSettingsProvider _integrationsSettingsProvider;

    public GenerateDiscordOauth2UriCommandHandler(ISender sender,
        IIntegrationsSettingsProvider integrationsSettingsProvider)
    {
        _sender = sender;
        _integrationsSettingsProvider = integrationsSettingsProvider;
    }

    public async Task<ErrorOr<GenerateDiscordOauth2UriCommandResult>> Handle(GenerateDiscordOauth2UriCommand command,
        CancellationToken cancellationToken)
    {
        var linkRequest =
            new CreateIntegrationLinkRequestCommand(command.UserId, CreateIntegrationLinkRequestCommandType.Discord);
        var linkRequestResult = await _sender.Send(linkRequest, cancellationToken);

        if (linkRequestResult.IsError) return linkRequestResult.Errors;

        var settings = _integrationsSettingsProvider.Discord;

        var state = Convert.ToBase64String(
            Encoding.UTF8.GetBytes(linkRequestResult.Value.IntegrationLinkRequestId.ToString()));

        var queryString = HttpUtility.ParseQueryString(String.Empty);
        queryString.Add("client_id", settings.ClientId);
        queryString.Add("redirect_uri", settings.RedirectUrl);
        queryString.Add("response_type", "code");
        queryString.Add("scope", string.Join(" ", settings.Scope));
        queryString.Add("state", state);

        var uri = new UriBuilder(settings.OAuth2Endpoint) { Query = queryString.ToString() }.Uri;

        return new GenerateDiscordOauth2UriCommandResult(uri);
    }
}
