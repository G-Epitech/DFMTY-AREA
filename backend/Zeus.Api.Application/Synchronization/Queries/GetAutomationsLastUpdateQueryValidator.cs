using FluentValidation;

namespace Zeus.Api.Application.Synchronization.Queries;

public sealed class GetAutomationsLastUpdateQueryValidator : AbstractValidator<GetAutomationsLastUpdateQuery>
{
    public GetAutomationsLastUpdateQueryValidator()
    {
        RuleFor(x => x.State).IsInEnum();
    }
}
