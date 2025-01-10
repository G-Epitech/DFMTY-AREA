using FluentValidation;

namespace Zeus.Api.Application.Authentication.Commands.GoogleAuth;

public class GoogleAuthCommandValidator : AbstractValidator<GoogleAuthCommand>
{
    public GoogleAuthCommandValidator()
    {
        RuleFor(x => x.Code).NotEmpty();
    }
}
