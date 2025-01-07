using ErrorOr;

using MapsterMapper;

using MediatR;

using Zeus.Api.Application.Integrations.Query.Results;
using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Application.Interfaces.Services.Integrations;
using Zeus.Api.Application.Interfaces.Services.Integrations.Discord;
using Zeus.Api.Domain.Errors.Integrations;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Application.Integrations.Query.Integrations.GetIntegration;

public class GetIntegrationQueryHandler : IRequestHandler<GetIntegrationQuery, ErrorOr<GetIntegrationQueryResult>>
{
    private readonly IIntegrationReadRepository _integrationReadRepository;
    private readonly IIntegrationService _integrationService;

    public GetIntegrationQueryHandler(IIntegrationReadRepository integrationReadRepository,
        IDiscordService discordService, IMapper mapper, IIntegrationService integrationService)
    {
        _integrationReadRepository = integrationReadRepository;
        _integrationService = integrationService;
    }

    public async Task<ErrorOr<GetIntegrationQueryResult>> Handle(GetIntegrationQuery query,
        CancellationToken cancellationToken)
    {
        var integrationId = new IntegrationId(query.IntegrationId);
        var userId = new UserId(query.UserId);

        var integration = await _integrationReadRepository.GetIntegrationByIdAsync(integrationId, cancellationToken);

        if (integration is null || integration.OwnerId != userId)
        {
            return Errors.Integrations.NotFound;
        }

        var propertiesResult = await _integrationService.GetProperties(integration);

        if (propertiesResult.IsError)
        {
            return propertiesResult.Errors;
        }

        return new GetIntegrationQueryResult(
            integration.Id.Value,
            integration.OwnerId.Value,
            integration.Type.ToString(),
            integration.IsValid,
            propertiesResult.Value);
    }
}
