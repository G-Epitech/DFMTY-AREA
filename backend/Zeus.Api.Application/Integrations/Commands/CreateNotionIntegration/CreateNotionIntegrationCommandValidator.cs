using FluentValidation;

namespace Zeus.Api.Application.Integrations.Commands.CreateNotionIntegration;

public class CreateNotionIntegrationCommandValidator : AbstractValidator<CreateNotionIntegrationCommand>
{
    public CreateNotionIntegrationCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty();
        RuleFor(x => x.State)
            .NotEmpty();
    }
}
