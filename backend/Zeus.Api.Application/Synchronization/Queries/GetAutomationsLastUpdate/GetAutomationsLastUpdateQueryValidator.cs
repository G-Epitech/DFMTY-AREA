using FluentValidation;

namespace Zeus.Api.Application.Synchronization.Queries.GetAutomationsLastUpdate;

public sealed class GetAutomationsLastUpdateQueryValidator : AbstractValidator<GetAutomationsLastUpdateQuery>
{
    public GetAutomationsLastUpdateQueryValidator()
    {
        RuleFor(x => x.State).IsInEnum();
    }
}
