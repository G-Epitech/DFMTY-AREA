using ErrorOr;

using MediatR;

namespace Zeus.Api.Application.Integrations.Commands.CreateLeagueOfLegendsIntegration;

public record CreateLeagueOfLegendsIntegrationCommand(
    Guid UserId,
    string GameName,
    string TagLine) : IRequest<ErrorOr<CreateLeagueOfLegendsIntegrationCommandResult>>;
