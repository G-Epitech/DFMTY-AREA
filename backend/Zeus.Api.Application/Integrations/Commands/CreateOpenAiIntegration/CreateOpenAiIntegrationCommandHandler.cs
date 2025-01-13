using ErrorOr;

using MediatR;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Application.Interfaces.Services.Integrations;
using Zeus.Common.Domain.Authentication.Common;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.Enums;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Application.Integrations.Commands.CreateOpenAiIntegration;

public class CreateOpenAiIntegrationCommandHandler : IRequestHandler<CreateOpenAiIntegrationCommand,
    ErrorOr<CreateOpenAiIntegrationCommandResult>>
{
    private readonly IIntegrationWriteRepository _integrationWriteRepository;
    private readonly IOpenAiService _openAiService;

    public CreateOpenAiIntegrationCommandHandler(IIntegrationWriteRepository integrationWriteRepository,
        IOpenAiService openAiService)
    {
        _integrationWriteRepository = integrationWriteRepository;
        _openAiService = openAiService;
    }

    public async Task<ErrorOr<CreateOpenAiIntegrationCommandResult>> Handle(CreateOpenAiIntegrationCommand command,
        CancellationToken cancellationToken)
    {
        var openAiModels = await _openAiService.GetModelsAsync(new AccessToken(command.ApiToken));
        if (openAiModels.IsError)
        {
            return openAiModels.Errors;
        }

        var openAiUsers = await _openAiService.GetUsersAsync(new AccessToken(command.AdminApiToken));
        if (openAiUsers.IsError)
        {
            return openAiUsers.Errors;
        }

        var userId = new UserId(command.UserId);
        var integration = OpenAiIntegration.Create(userId, "");

        integration.AddToken(new IntegrationToken(command.ApiToken, "Bearer",
            IntegrationTokenUsage.Access));
        integration.AddToken(new IntegrationToken(command.AdminApiToken, "Admin",
            IntegrationTokenUsage.Access));

        await _integrationWriteRepository.AddIntegrationAsync(integration, cancellationToken);

        return new CreateOpenAiIntegrationCommandResult(integration.Id.Value);
    }
}
