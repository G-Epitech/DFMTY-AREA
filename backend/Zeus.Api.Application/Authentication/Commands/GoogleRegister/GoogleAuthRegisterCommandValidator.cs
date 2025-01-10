using FluentValidation;

namespace Zeus.Api.Application.Authentication.Commands.GoogleRegister;

public class GoogleAuthRegisterCommandValidator : AbstractValidator<GoogleAuthRegisterCommand>
{
    public GoogleAuthRegisterCommandValidator()
    {
        RuleFor(x => x.AccessToken).NotNull();
        RuleFor(x => x.RefreshToken).NotNull();
        RuleFor(x => x.GoogleUser).NotNull();
    }
}
