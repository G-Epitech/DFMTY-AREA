using FluentValidation;

namespace Zeus.Api.Application.Authentication.Commands.GoogleAuthFromCode;

public class GoogleAuthFromCodeCommandValidator : AbstractValidator<GoogleAuthFromCodeCommand>
{
    public GoogleAuthFromCodeCommandValidator()
    {
        RuleFor(x => x.Code).NotEmpty();
    }
}
