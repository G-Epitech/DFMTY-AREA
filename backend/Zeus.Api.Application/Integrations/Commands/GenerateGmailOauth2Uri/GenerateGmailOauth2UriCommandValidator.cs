using FluentValidation;

namespace Zeus.Api.Application.Integrations.Commands.GenerateGmailOauth2Uri;

public class GenerateGmailOauth2UriCommandValidator : AbstractValidator<GenerateGmailOauth2UriCommand>
{
    public GenerateGmailOauth2UriCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}
