using FluentValidation;

namespace Zeus.Api.Application.Integrations.Commands.GenerateDiscordOauth2Uri;

public class GenerateDiscordOauth2UriCommandValidator : AbstractValidator<GenerateDiscordOauth2UriCommand>
{
    public GenerateDiscordOauth2UriCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}
