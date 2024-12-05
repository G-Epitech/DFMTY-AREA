using ErrorOr;

using MapsterMapper;

using MediatR;

using Zeus.Api.Application.Integrations.Query.GetIntegration.Results;
using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Application.Interfaces.Services.Integrations.Discord;
using Zeus.Api.Domain.Authentication.ValueObjects;
using Zeus.Api.Domain.Errors.Integrations;
using Zeus.Api.Domain.Integrations.Enums;
using Zeus.Api.Domain.Integrations.IntegrationAggregate;
using Zeus.Api.Domain.Integrations.IntegrationAggregate.Enums;
using Zeus.Api.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Api.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Application.Integrations.Query.GetIntegration;

public class GetIntegrationQueryHandler : IRequestHandler<GetIntegrationQuery, ErrorOr<GetIntegrationQueryResult>>
{
    private readonly IIntegrationReadRepository _integrationReadRepository;
    private readonly IDiscordService _discordService;
    private readonly IMapper _mapper;

    public GetIntegrationQueryHandler(IIntegrationReadRepository integrationReadRepository,
        IDiscordService discordService, IMapper mapper)
    {
        _integrationReadRepository = integrationReadRepository;
        _discordService = discordService;
        _mapper = mapper;
    }

    private async Task<ErrorOr<GetIntegrationPropertiesQueryResult>> GetDiscordIntegrationProperties(
        Integration integration)
    {
        var discordIntegration = (DiscordIntegration)integration;

        var accessToken = discordIntegration.Tokens.First(x => x.Usage == ServiceTokenUsage.Access);
        var discordUser = await _discordService.GetUserAsync(new AccessToken(accessToken.Value));

        if (discordUser.IsError)
        {
            return discordUser.Errors;
        }

        return _mapper.Map<GetDiscordIntegrationPropertiesQueryResult>(discordUser.Value);
    }

    public async Task<ErrorOr<GetIntegrationQueryResult>> Handle(GetIntegrationQuery query,
        CancellationToken cancellationToken)
    {
        var integrationId = new IntegrationId(query.IntegrationId);
        var userId = new UserId(query.UserId);

        var integration = await _integrationReadRepository.GetIntegrationByIdAsync(integrationId);

        if (integration is null || integration.OwnerId != userId)
        {
            return Errors.Integrations.NotFound;
        }

        var propertiesResult = integration.Type switch
        {
            IntegrationType.Discord => await GetDiscordIntegrationProperties(integration),
            _ => Errors.Integrations.PropertiesHandlerNotFound
        };
        
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
