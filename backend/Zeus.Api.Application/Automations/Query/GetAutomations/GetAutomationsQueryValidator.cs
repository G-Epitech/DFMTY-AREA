using FluentValidation;

namespace Zeus.Api.Application.Automations.Query.GetAutomations;

public class GetAutomationsQueryValidator : AbstractValidator<GetAutomationsQuery>
{
    public GetAutomationsQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}
