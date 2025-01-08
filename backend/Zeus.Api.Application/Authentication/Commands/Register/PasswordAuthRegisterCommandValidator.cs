using FluentValidation;

namespace Zeus.Api.Application.Authentication.Commands.Register;

public class PasswordAuthRegisterCommandValidator: AbstractValidator<PasswordAuthRegisterCommand>
{
    public PasswordAuthRegisterCommandValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).MinimumLength(8);
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
    }
}
