using FluentValidation;

namespace Zeus.Api.Application.Integrations.Commands.CreateOpenAiIntegration;

public class CreateOpenAiIntegrationCommandValidator : AbstractValidator<CreateOpenAiIntegrationCommand>
{
    public CreateOpenAiIntegrationCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
        RuleFor(x => x.ApiToken)
            .NotEmpty();
    }
}
