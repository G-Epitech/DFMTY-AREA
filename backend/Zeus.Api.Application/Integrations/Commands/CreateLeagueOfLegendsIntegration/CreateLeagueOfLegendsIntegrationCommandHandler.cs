using ErrorOr;

using MediatR;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Application.Interfaces.Services.Integrations;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Application.Integrations.Commands.CreateLeagueOfLegendsIntegration;

public class CreateLeagueOfLegendsIntegrationCommandHandler : IRequestHandler<CreateLeagueOfLegendsIntegrationCommand,
    ErrorOr<CreateLeagueOfLegendsIntegrationCommandResult>>
{
    private readonly IIntegrationWriteRepository _integrationWriteRepository;
    private readonly ILeagueOfLegendsService _leagueOfLegendsServiceService;

    public CreateLeagueOfLegendsIntegrationCommandHandler(IIntegrationWriteRepository integrationWriteRepository,
        ILeagueOfLegendsService leagueOfLegendsService)
    {
        _integrationWriteRepository = integrationWriteRepository;
        _leagueOfLegendsServiceService = leagueOfLegendsService;
    }

    public async Task<ErrorOr<CreateLeagueOfLegendsIntegrationCommandResult>> Handle(
        CreateLeagueOfLegendsIntegrationCommand command, CancellationToken cancellationToken)
    {
        var leagueOfLegendsAccount =
            await _leagueOfLegendsServiceService.GetAccountByRiotIdAsync(command.GameName, command.TagLine);

        if (leagueOfLegendsAccount.IsError)
        {
            return leagueOfLegendsAccount.Errors;
        }

        var userId = new UserId(command.UserId);
        var integration = LeagueOfLegendsIntegration.Create(userId, leagueOfLegendsAccount.Value.Id.Value);

        await _integrationWriteRepository.AddIntegrationAsync(integration, cancellationToken);

        return new CreateLeagueOfLegendsIntegrationCommandResult(integration.Id.Value);
    }
}
