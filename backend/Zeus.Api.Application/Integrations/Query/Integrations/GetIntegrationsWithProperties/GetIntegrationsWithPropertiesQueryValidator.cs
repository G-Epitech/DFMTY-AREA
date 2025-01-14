using FluentValidation;

namespace Zeus.Api.Application.Integrations.Query.Integrations.GetIntegrationsWithProperties;

public class GetIntegrationsWithPropertiesQueryValidator : AbstractValidator<GetIntegrationsWithPropertiesQuery>
{
    public GetIntegrationsWithPropertiesQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
        RuleFor(x => x.Index).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Limit).GreaterThan(0);
    }
}
