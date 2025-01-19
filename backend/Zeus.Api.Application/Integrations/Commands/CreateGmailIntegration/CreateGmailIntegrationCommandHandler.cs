using ErrorOr;

using MediatR;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Application.Interfaces.Services.Integrations;
using Zeus.Api.Domain.Errors.Integrations;
using Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate.ValueObjects;
using Zeus.Common.Domain.Integrations.Common.Enums;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.Enums;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;

namespace Zeus.Api.Application.Integrations.Commands.CreateGmailIntegration;

public class CreateGmailIntegrationCommandHandler : IRequestHandler<CreateGmailIntegrationCommand,
    ErrorOr<CreateGmailIntegrationCommandResult>>
{
    private readonly IGmailService _gmailService;
    private readonly IIntegrationLinkRequestReadRepository _integrationLinkRequestReadRepository;
    private readonly IIntegrationLinkRequestWriteRepository _integrationLinkRequestWriteRepository;
    private readonly IIntegrationWriteRepository _integrationWriteRepository;

    public CreateGmailIntegrationCommandHandler(
        IIntegrationLinkRequestReadRepository integrationLinkRequestReadRepository,
        IIntegrationLinkRequestWriteRepository integrationLinkRequestWriteRepository,
        IIntegrationWriteRepository integrationWriteRepository,
        IGmailService gmailService)
    {
        _integrationWriteRepository = integrationWriteRepository;
        _integrationLinkRequestReadRepository = integrationLinkRequestReadRepository;
        _integrationLinkRequestWriteRepository = integrationLinkRequestWriteRepository;
        _gmailService = gmailService;
    }

    public async Task<ErrorOr<CreateGmailIntegrationCommandResult>> Handle(CreateGmailIntegrationCommand command,
        CancellationToken cancellationToken)
    {
        var linkRequestId = new IntegrationLinkRequestId(Guid.Parse(command.State));

        var linkRequest = await _integrationLinkRequestReadRepository.GetRequestByIdAsync(linkRequestId, cancellationToken);
        if (linkRequest is null || linkRequest.Type != IntegrationType.Gmail)
        {
            return Errors.Integrations.Gmail.InvalidLinkRequest;
        }

        var getTokensRes = await _gmailService.GetTokensFromOauth2Async(command.Code);
        if (getTokensRes.IsError)
        {
            return getTokensRes.Errors;
        }

        var gmailTokens = getTokensRes.Value;
        var getUserRes = await _gmailService.GetUserAsync(gmailTokens.AccessToken);
        if (getUserRes.IsError)
        {
            return getUserRes.Errors;
        }

        var gmailUser = getUserRes.Value;

        var integration = GmailIntegration.Create(linkRequest.OwnerId, gmailUser.Id.Value);
        integration.AddToken(new IntegrationToken(gmailTokens.AccessToken.Value,
            getTokensRes.Value.TokenType, IntegrationTokenUsage.Access));
        integration.AddToken(new IntegrationToken(gmailTokens.RefreshToken.Value,
            getTokensRes.Value.TokenType, IntegrationTokenUsage.Refresh));

        await _integrationWriteRepository.AddIntegrationAsync(integration, cancellationToken);

        await _integrationLinkRequestWriteRepository.DeleteRequestAsync(linkRequest, cancellationToken);

        return new CreateGmailIntegrationCommandResult(integration.Id.Value);
    }
}
