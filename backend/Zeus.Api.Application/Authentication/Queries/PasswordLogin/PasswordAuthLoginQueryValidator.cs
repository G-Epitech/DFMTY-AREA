using FluentValidation;

namespace Zeus.Api.Application.Authentication.Queries.PasswordLogin;

public class PasswordAuthLoginQueryValidator : AbstractValidator<PasswordAuthLoginQuery>
{
    public PasswordAuthLoginQueryValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}
