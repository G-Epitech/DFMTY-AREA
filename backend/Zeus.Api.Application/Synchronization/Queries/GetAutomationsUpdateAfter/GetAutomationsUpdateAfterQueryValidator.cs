using FluentValidation;

namespace Zeus.Api.Application.Synchronization.Queries.GetAutomationsUpdateAfter;

public sealed class GetAutomationsUpdateAfterQueryValidator : AbstractValidator<GetAutomationsUpdateAfterQuery>
{
    public GetAutomationsUpdateAfterQueryValidator()
    {
        RuleFor(x => x.State).IsInEnum();
    }
}
