using FluentValidation;

namespace Zeus.Api.Application.Authentication.Commands.GoogleAuthFromCredentials;

public class GoogleAuthFromCredentialsCommandValidator : AbstractValidator<GoogleAuthFromCredentialsCommand>
{
    public GoogleAuthFromCredentialsCommandValidator()
    {
        RuleFor(x => x.AccessToken.Value).NotEmpty();
        RuleFor(x => x.RefreshToken.Value).NotEmpty();
        RuleFor(x => x.TokenType).NotEmpty();
    }
}
