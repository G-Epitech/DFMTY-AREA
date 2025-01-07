using FluentValidation;

namespace Zeus.Api.Application.Integrations.Query.GetDiscordGuilds;

public class GetDiscordGuildsQueryValidator : AbstractValidator<GetDiscordGuildsQuery>
{
    public GetDiscordGuildsQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
        RuleFor(x => x.IntegrationId)
            .NotEmpty();
    }
}
