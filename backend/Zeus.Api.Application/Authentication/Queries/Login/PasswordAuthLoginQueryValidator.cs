using FluentValidation;

namespace Zeus.Api.Application.Authentication.Queries.Login;

public class PasswordAuthLoginQueryValidator : AbstractValidator<PasswordAuthLoginQuery>
{
    public PasswordAuthLoginQueryValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}
