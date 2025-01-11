using FluentValidation;

namespace Zeus.Api.Application.Integrations.Commands.GenerateNotionOauth2Uri;

public class GenerateNotionOauth2UriCommandValidator : AbstractValidator<GenerateNotionOauth2UriCommand>
{
    public GenerateNotionOauth2UriCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}
