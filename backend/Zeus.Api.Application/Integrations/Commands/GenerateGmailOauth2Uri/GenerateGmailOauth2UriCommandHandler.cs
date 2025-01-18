using System.Web;

using ErrorOr;

using MediatR;

using Zeus.Api.Application.Integrations.Commands.CreateIntegrationLinkRequest;
using Zeus.Api.Application.Interfaces.Services.Settings.Integrations;

namespace Zeus.Api.Application.Integrations.Commands.GenerateGmailOauth2Uri;

public class GenerateGmailOauth2UriCommandHandler : IRequestHandler<GenerateGmailOauth2UriCommand,
    ErrorOr<GenerateGmailOauth2UriCommandResult>>
{
    private readonly IIntegrationsSettingsProvider _integrationsSettingsProvider;
    private readonly ISender _sender;

    public GenerateGmailOauth2UriCommandHandler(ISender sender,
        IIntegrationsSettingsProvider integrationsSettingsProvider)
    {
        _sender = sender;
        _integrationsSettingsProvider = integrationsSettingsProvider;
    }

    public async Task<ErrorOr<GenerateGmailOauth2UriCommandResult>> Handle(GenerateGmailOauth2UriCommand command,
        CancellationToken cancellationToken)
    {
        var linkRequest =
            new CreateIntegrationLinkRequestCommand(command.UserId, CreateIntegrationLinkRequestCommandType.Gmail);
        var linkRequestResult = await _sender.Send(linkRequest, cancellationToken);

        if (linkRequestResult.IsError) return linkRequestResult.Errors;

        var settings = _integrationsSettingsProvider.Gmail;

        var queryString = HttpUtility.ParseQueryString(String.Empty);
        queryString.Add("redirect_uri", settings.RedirectUrl);
        queryString.Add("prompt", "consent");
        queryString.Add("response_type", "code");
        queryString.Add("client_id", settings.ClientId);
        queryString.Add("scope", string.Join(' ', settings.Scopes));
        queryString.Add("state", linkRequestResult.Value.IntegrationLinkRequestId.ToString());
        queryString.Add("access_type", settings.OAuth2AccessType);

        var builder = new UriBuilder(settings.OAuth2Endpoint) { Query = queryString.ToString() };

        return new GenerateGmailOauth2UriCommandResult(builder.Uri);
    }
}
