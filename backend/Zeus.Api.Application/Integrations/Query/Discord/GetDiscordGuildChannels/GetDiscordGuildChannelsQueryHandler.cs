using ErrorOr;

using MediatR;

using Zeus.Api.Application.Interfaces.Repositories;
using Zeus.Api.Application.Interfaces.Services.Integrations.Discord;
using Zeus.Api.Application.Interfaces.Services.Settings.Integrations;
using Zeus.Api.Domain.Errors.Integrations;
using Zeus.Api.Domain.Integrations.Discord.ValueObjects;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.ValueObjects;
using Zeus.Common.Domain.UserAggregate.ValueObjects;

namespace Zeus.Api.Application.Integrations.Query.Discord.GetDiscordGuildChannels;

public class GetDiscordGuildChannelsQueryHandler : IRequestHandler<GetDiscordGuildChannelsQuery,
    ErrorOr<List<GetDiscordGuildChannelQueryResult>>>
{
    private readonly IIntegrationReadRepository _integrationReadRepository;
    private readonly IDiscordService _discordService;
    private readonly IIntegrationsSettingsProvider _integrationsSettingsProvider;

    public GetDiscordGuildChannelsQueryHandler(IIntegrationReadRepository integrationReadRepository,
        IDiscordService discordService, IIntegrationsSettingsProvider integrationsSettingsProvider)
    {
        _integrationReadRepository = integrationReadRepository;
        _discordService = discordService;
        _integrationsSettingsProvider = integrationsSettingsProvider;
    }

    public async Task<ErrorOr<List<GetDiscordGuildChannelQueryResult>>> Handle(GetDiscordGuildChannelsQuery query,
        CancellationToken cancellationToken)
    {
        var integrationId = new IntegrationId(query.IntegrationId);
        var userId = new UserId(query.UserId);

        var integration = await _integrationReadRepository.GetIntegrationByIdAsync(integrationId, cancellationToken);

        if (integration is null || integration.OwnerId != userId)
        {
            return Errors.Integrations.NotFound;
        }

        var discordGuildId = new DiscordGuildId(query.GuildId);
        var guildChannels =
            await _discordService.GetGuildChannelsAsync(discordGuildId, _integrationsSettingsProvider.Discord.BotToken);

        if (guildChannels.IsError)
        {
            return guildChannels.Errors;
        }

        return guildChannels.Value.Where(channel => channel.Type == DiscordChannelType.GuildText).Select(channel =>
            new GetDiscordGuildChannelQueryResult(
                channel.Id.ValueString,
                channel.Name)).ToList();
    }
}
