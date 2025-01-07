using FluentValidation;

namespace Zeus.Api.Application.Integrations.Query.Discord.GetDiscordGuildChannels;

public class GetDiscordGuildChannelsQueryValidator : AbstractValidator<GetDiscordGuildChannelsQuery>
{
    public GetDiscordGuildChannelsQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
        RuleFor(x => x.IntegrationId)
            .NotEmpty();
        RuleFor(x => x.GuildId)
            .NotEmpty().MinimumLength(18);
    }
}
