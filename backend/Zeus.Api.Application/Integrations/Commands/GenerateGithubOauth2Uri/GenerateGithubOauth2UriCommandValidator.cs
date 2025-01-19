using FluentValidation;

namespace Zeus.Api.Application.Integrations.Commands.GenerateGithubOauth2Uri;

public class GenerateGithubOauth2UriCommandValidator : AbstractValidator<GenerateGithubOauth2UriCommand>
{
    public GenerateGithubOauth2UriCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}
