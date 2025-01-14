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

namespace Zeus.Api.Application.Integrations.Commands.CreateNotionIntegration;

public class CreateNotionIntegrationCommandHandler : IRequestHandler<CreateNotionIntegrationCommand,
    ErrorOr<CreateNotionIntegrationCommandResult>>
{
    private readonly IIntegrationLinkRequestReadRepository _integrationLinkRequestReadRepository;
    private readonly IIntegrationLinkRequestWriteRepository _integrationLinkRequestWriteRepository;
    private readonly IIntegrationWriteRepository _integrationWriteRepository;
    private readonly INotionService _notionService;

    public CreateNotionIntegrationCommandHandler(
        IIntegrationLinkRequestReadRepository integrationLinkRequestReadRepository,
        IIntegrationLinkRequestWriteRepository integrationLinkRequestWriteRepository,
        IIntegrationWriteRepository integrationWriteRepository, INotionService notionService)
    {
        _integrationLinkRequestReadRepository = integrationLinkRequestReadRepository;
        _integrationLinkRequestWriteRepository = integrationLinkRequestWriteRepository;
        _integrationWriteRepository = integrationWriteRepository;
        _notionService = notionService;
    }

    public async Task<ErrorOr<CreateNotionIntegrationCommandResult>> Handle(CreateNotionIntegrationCommand command,
        CancellationToken cancellationToken)
    {
        var linkRequestId = new IntegrationLinkRequestId(Guid.Parse(command.State));

        var linkRequest =
            await _integrationLinkRequestReadRepository.GetRequestByIdAsync(linkRequestId, cancellationToken);
        if (linkRequest is null || linkRequest.Type != IntegrationType.Notion)
        {
            return Errors.Integrations.Notion.InvalidLinkRequest;
        }

        var notionWorkspaceTokens = await _notionService.GetTokensFromOauth2Async(command.Code);
        if (notionWorkspaceTokens.IsError)
        {
            return notionWorkspaceTokens.Errors;
        }

        var integration = NotionIntegration.Create(linkRequest.OwnerId, notionWorkspaceTokens.Value.WorkspaceId.Value);
        integration.AddToken(new IntegrationToken(notionWorkspaceTokens.Value.AccessToken.Value, "Bearer",
            IntegrationTokenUsage.Access));

        await _integrationWriteRepository.AddIntegrationAsync(integration, cancellationToken);

        await _integrationLinkRequestWriteRepository.DeleteRequestAsync(linkRequest, cancellationToken);

        return new CreateNotionIntegrationCommandResult(integration.Id.Value);
    }
}
