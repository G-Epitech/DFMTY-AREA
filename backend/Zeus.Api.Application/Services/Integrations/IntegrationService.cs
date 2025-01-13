using ErrorOr;

using MapsterMapper;

using Zeus.Api.Application.Interfaces.Services.Integrations;
using Zeus.Api.Domain.Errors.Integrations;
using Zeus.Api.Domain.Integrations.Properties;
using Zeus.Common.Domain.Authentication.Common;
using Zeus.Common.Domain.Integrations.Common.Enums;
using Zeus.Common.Domain.Integrations.IntegrationAggregate;
using Zeus.Common.Domain.Integrations.IntegrationAggregate.Enums;

namespace Zeus.Api.Application.Services.Integrations;

public class IntegrationService : IIntegrationService
{
    private readonly IDiscordService _discordService;
    private readonly INotionService _notionService;
    private readonly IOpenAiService _openAiService;
    private readonly IMapper _mapper;

    public IntegrationService(IDiscordService discordService, INotionService notionService, IMapper mapper,
        IOpenAiService openAiService)
    {
        _discordService = discordService;
        _notionService = notionService;
        _mapper = mapper;
        _openAiService = openAiService;
    }

    public async Task<ErrorOr<IntegrationProperties>> GetProperties(Integration integration)
    {
        return integration.Type switch
        {
            IntegrationType.Discord => await GetIntegrationDiscordProperties(integration),
            IntegrationType.Notion => await GetIntegrationNotionProperties(integration),
            IntegrationType.OpenAi => await GetIntegrationOpenAiProperties(integration),
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

    private async Task<ErrorOr<IntegrationProperties>> GetIntegrationNotionProperties(
        Integration integration)
    {
        var notionIntegration = (NotionIntegration)integration;

        var accessToken = notionIntegration.Tokens.First(x => x.Usage == IntegrationTokenUsage.Access);
        var notionBot = await _notionService.GetBotAsync(new AccessToken(accessToken.Value));

        if (notionBot.IsError)
        {
            return notionBot.Errors;
        }

        return _mapper.Map<IntegrationNotionProperties>(notionBot.Value);
    }

    private async Task<ErrorOr<IntegrationProperties>> GetIntegrationOpenAiProperties(
        Integration integration)
    {
        var openAiIntegration = (OpenAiIntegration)integration;

        var accessToken =
            openAiIntegration.Tokens.First(x => x is { Usage: IntegrationTokenUsage.Access, Type: "Admin" });
        var users = await _openAiService.GetUsersAsync(new AccessToken(accessToken.Value));

        if (users.IsError)
        {
            return users.Errors;
        }

        var owner = users.Value.FirstOrDefault(user => user.Role == "owner");
        if (owner is null)
        {
            return Errors.Integrations.OpenAi.OwnerNotFound;
        }

        return _mapper.Map<IntegrationOpenAiProperties>(owner);
    }
}
