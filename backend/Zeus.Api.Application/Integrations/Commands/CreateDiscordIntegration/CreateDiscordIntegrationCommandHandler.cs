using System.Text;

using ErrorOr;

using MediatR;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Application.Interfaces.Services.Integrations.Discord;
using Zeus.Api.Domain.Errors.Integrations;
using Zeus.Api.Domain.Integrations.Enums;
using Zeus.Api.Domain.Integrations.IntegrationAggregate;
using Zeus.Api.Domain.Integrations.IntegrationLinkRequestAggregate.ValueObjects;

namespace Zeus.Api.Application.Integrations.Commands.CreateDiscordIntegration;

public class CreateDiscordIntegrationCommandHandler : IRequestHandler<CreateDiscordIntegrationCommand,
    ErrorOr<CreateDiscordIntegrationCommandResult>>
{
    private readonly IIntegrationReadRepository _integrationReadRepository;
    private readonly IIntegrationWriteRepository _integrationWriteRepository;
    private readonly IDiscordService _discordService;

    public CreateDiscordIntegrationCommandHandler(IIntegrationReadRepository integrationReadRepository,
        IIntegrationWriteRepository integrationWriteRepository, IDiscordService discordService)
    {
        _integrationReadRepository = integrationReadRepository;
        _integrationWriteRepository = integrationWriteRepository;
        _discordService = discordService;
    }

    public async Task<ErrorOr<CreateDiscordIntegrationCommandResult>> Handle(CreateDiscordIntegrationCommand command,
        CancellationToken cancellationToken)
    {
        var linkRequestStringId = Encoding.UTF8.GetString(Convert.FromBase64String(command.State));
        var linkRequestId = new IntegrationLinkRequestId(Guid.Parse(linkRequestStringId));

        var linkRequest = await _integrationReadRepository.GetIntegrationLinkRequestByIdAsync(linkRequestId);
        if (linkRequest is null || linkRequest.Type != IntegrationType.Discord)
        {
            return Errors.Integrations.Discord.InvalidLinkRequest;
        }

        var discordTokens = await _discordService.GetTokensFromOauth2Async(command.Code);
        if (discordTokens.IsError)
        {
            return discordTokens.Errors;
        }

        var discordUser = await _discordService.GetUserAsync(discordTokens.Value.AccessToken);
        if (discordUser.IsError)
        {
            return discordUser.Errors;
        }

        var integration = DiscordIntegration.Create(linkRequest.OwnerId, discordUser.Value.Id.ValueString);
        await _integrationWriteRepository.AddIntegrationAsync(integration);

        await _integrationWriteRepository.DeleteIntegrationLinkRequestAsync(linkRequest);

        return new CreateDiscordIntegrationCommandResult(integration.Id.Value);
    }
}
