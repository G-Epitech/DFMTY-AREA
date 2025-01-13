using ErrorOr;

using MediatR;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Application.Interfaces.Services.Integrations;
using Zeus.Api.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Api.Domain.Errors.Integrations;
using Zeus.Common.Domain.Authentication.Common;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.Enums;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Application.Integrations.Query.Discord.GetDiscordUserGuilds;

public class
    GetDiscordUserGuildsQueryHandler : IRequestHandler<GetDiscordUserGuildsQuery, ErrorOr<List<GetDiscordUserGuildQueryResult>>>
{
    private readonly IIntegrationReadRepository _integrationReadRepository;
    private readonly IDiscordService _discordService;
    private readonly IIntegrationsSettingsProvider _integrationsSettingsProvider;

    public GetDiscordUserGuildsQueryHandler(IDiscordService discordService,
        IIntegrationReadRepository integrationReadRepository,
        IIntegrationsSettingsProvider integrationsSettingsProvider)
    {
        _discordService = discordService;
        _integrationReadRepository = integrationReadRepository;
        _integrationsSettingsProvider = integrationsSettingsProvider;
    }

    public async Task<ErrorOr<List<GetDiscordUserGuildQueryResult>>> Handle(GetDiscordUserGuildsQuery query,
        CancellationToken cancellationToken)
    {
        var integrationId = new IntegrationId(query.IntegrationId);
        var userId = new UserId(query.UserId);

        var integration = await _integrationReadRepository.GetIntegrationByIdAsync(integrationId, cancellationToken);

        if (integration is null || integration.OwnerId != userId)
        {
            return Errors.Integrations.NotFound;
        }

        var accessToken = integration.Tokens.First(x => x.Usage == IntegrationTokenUsage.Access);
        var userGuilds = await _discordService.GetUserGuildsAsync(new AccessToken(accessToken.Value));

        if (userGuilds.IsError)
        {
            return userGuilds.Errors;
        }

        var botGuilds = await _discordService.GetBotGuildsAsync(_integrationsSettingsProvider.Discord.BotToken);
        
        if (botGuilds.IsError)
        {
            return botGuilds.Errors;
        }

        return userGuilds.Value.Select(guild => new GetDiscordUserGuildQueryResult(
            guild.Id.ValueString,
            guild.Name,
            guild.IconUri,
            guild.ApproximateMemberCount,
            botGuilds.Value.Any(botGuild => botGuild.Id == guild.Id))).ToList();
    }
}
