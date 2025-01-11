using System.Web;

using ErrorOr;

using MediatR;

using Zeus.Api.Application.Integrations.Commands.CreateIntegrationLinkRequest;
using Zeus.Api.Application.Interfaces.Services.Settings.Integrations;

namespace Zeus.Api.Application.Integrations.Commands.GenerateNotionOauth2Uri;

public class GenerateNotionOauth2UriCommandHandler : IRequestHandler<GenerateNotionOauth2UriCommand,
    ErrorOr<GenerateNotionOauth2UriCommandResult>>
{
    private readonly ISender _sender;
    private readonly IIntegrationsSettingsProvider _integrationsSettingsProvider;

    public GenerateNotionOauth2UriCommandHandler(ISender sender,
        IIntegrationsSettingsProvider integrationsSettingsProvider)
    {
        _sender = sender;
        _integrationsSettingsProvider = integrationsSettingsProvider;
    }

    public async Task<ErrorOr<GenerateNotionOauth2UriCommandResult>> Handle(GenerateNotionOauth2UriCommand command,
        CancellationToken cancellationToken)
    {
        var linkRequest =
            new CreateIntegrationLinkRequestCommand(command.UserId, CreateIntegrationLinkRequestCommandType.Notion);
        var linkRequestResult = await _sender.Send(linkRequest, cancellationToken);

        if (linkRequestResult.IsError) return linkRequestResult.Errors;

        var settings = _integrationsSettingsProvider.Notion;

        var queryString = HttpUtility.ParseQueryString(String.Empty);
        queryString.Add("client_id", settings.ClientId);
        queryString.Add("redirect_uri", settings.RedirectUrl);
        queryString.Add("response_type", "code");
        queryString.Add("owner", "user");
        queryString.Add("state", linkRequestResult.Value.IntegrationLinkRequestId.ToString());

        var uri = new UriBuilder(settings.OAuth2Endpoint) { Query = queryString.ToString() }.Uri;

        return new GenerateNotionOauth2UriCommandResult(uri);
    }
}
