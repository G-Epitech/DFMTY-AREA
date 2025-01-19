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

namespace Zeus.Api.Application.Integrations.Commands.CreateGithubIntegration;

public class CreateGithubIntegrationCommandHandler : IRequestHandler<CreateGithubIntegrationCommand,
    ErrorOr<CreateGithubIntegrationCommandResult>>
{
    private readonly IIntegrationLinkRequestReadRepository _integrationLinkRequestReadRepository;
    private readonly IIntegrationLinkRequestWriteRepository _integrationLinkRequestWriteRepository;
    private readonly IIntegrationWriteRepository _integrationWriteRepository;
    private readonly IGithubService _githubService;

    public CreateGithubIntegrationCommandHandler(
        IIntegrationLinkRequestReadRepository integrationLinkRequestReadRepository,
        IIntegrationLinkRequestWriteRepository integrationLinkRequestWriteRepository,
        IIntegrationWriteRepository integrationWriteRepository, IGithubService githubService)
    {
        _integrationLinkRequestReadRepository = integrationLinkRequestReadRepository;
        _integrationLinkRequestWriteRepository = integrationLinkRequestWriteRepository;
        _integrationWriteRepository = integrationWriteRepository;
        _githubService = githubService;
    }

    public async Task<ErrorOr<CreateGithubIntegrationCommandResult>> Handle(CreateGithubIntegrationCommand command,
        CancellationToken cancellationToken)
    {
        var linkRequestId = new IntegrationLinkRequestId(Guid.Parse(command.State));

        var linkRequest =
            await _integrationLinkRequestReadRepository.GetRequestByIdAsync(linkRequestId, cancellationToken);
        if (linkRequest is null || linkRequest.Type != IntegrationType.Github)
        {
            return Errors.Integrations.Notion.InvalidLinkRequest;
        }

        var githubTokens = await _githubService.GetTokensFromOauth2Async(command.Code);
        if (githubTokens.IsError)
        {
            return githubTokens.Errors;
        }
        
        // TODO: Get github user

        var integration = GithubIntegration.Create(linkRequest.OwnerId, "");
        integration.AddToken(new IntegrationToken(githubTokens.Value.AccessToken.Value, "Bearer",
            IntegrationTokenUsage.Access));

        await _integrationWriteRepository.AddIntegrationAsync(integration, cancellationToken);

        await _integrationLinkRequestWriteRepository.DeleteRequestAsync(linkRequest, cancellationToken);

        return new CreateGithubIntegrationCommandResult(integration.Id.Value);
    }
}
