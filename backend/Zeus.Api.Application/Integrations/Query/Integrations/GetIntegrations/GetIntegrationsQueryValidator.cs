using FluentValidation;

namespace Zeus.Api.Application.Integrations.Query.Integrations.GetIntegrations;

public class GetIntegrationsQueryValidator : AbstractValidator<GetIntegrationsQuery>
{
    public GetIntegrationsQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
        RuleFor(x => x.Index).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Limit).GreaterThan(0);
    }
}
