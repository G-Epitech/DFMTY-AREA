using ErrorOr;

using MapsterMapper;

using Zeus.Api.Application.Interfaces.Services.Integrations;
using Zeus.Api.Application.Interfaces.Services.Integrations.Discord;
using Zeus.Api.Domain.Authentication.ValueObjects;
using Zeus.Api.Domain.Errors.Integrations;
using Zeus.Api.Domain.Integrations.Common.Enums;
using Zeus.Api.Domain.Integrations.IntegrationAggregate;
using Zeus.Api.Domain.Integrations.IntegrationAggregate.Enums;
using Zeus.Api.Domain.Integrations.Properties;

namespace Zeus.Api.Application.Services.Integrations;

public class IntegrationService : IIntegrationService
{
    private readonly IDiscordService _discordService;
    private readonly IMapper _mapper;

    public IntegrationService(IDiscordService discordService, IMapper mapper)
    {
        _discordService = discordService;
        _mapper = mapper;
    }

    public async Task<ErrorOr<IntegrationProperties>> GetProperties(Integration integration)
    {
        return integration.Type switch
        {
            IntegrationType.Discord => await GetIntegrationDiscordProperties(integration),
            _ => Errors.Integrations.PropertiesHandlerNotFound
        };
    }

    private async Task<ErrorOr<IntegrationProperties>> GetIntegrationDiscordProperties(
        Integration integration)
    {
        var discordIntegration = (DiscordIntegration)integration;

        var accessToken = discordIntegration.Tokens.First(x => x.Usage == IntegrationTokenUsage.Access);
        var discordUser = await _discordService.GetUserAsync(new AccessToken(accessToken.Value));

        if (discordUser.IsError)
        {
            return discordUser.Errors;
        }

        return _mapper.Map<IntegrationDiscordProperties>(discordUser.Value);
    }
}
