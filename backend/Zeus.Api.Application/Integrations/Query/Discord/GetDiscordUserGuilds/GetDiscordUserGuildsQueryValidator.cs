using FluentValidation;

namespace Zeus.Api.Application.Integrations.Query.Discord.GetDiscordUserGuilds;

public class GetDiscordUserGuildsQueryValidator : AbstractValidator<GetDiscordUserGuildsQuery>
{
    public GetDiscordUserGuildsQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
        RuleFor(x => x.IntegrationId)
            .NotEmpty();
    }
}
