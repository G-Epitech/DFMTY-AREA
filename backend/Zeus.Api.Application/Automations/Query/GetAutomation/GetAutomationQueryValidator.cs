using FluentValidation;

namespace Zeus.Api.Application.Automations.Query.GetAutomation;

public class GetAutomationQueryValidator : AbstractValidator<GetAutomationQuery>
{
    public GetAutomationQueryValidator()
    {
        RuleFor(x => x.AutomationId)
            .NotEmpty();
    }
}
