using FluentValidation;

namespace Zeus.Api.Application.Integrations.Commands.CreateGmailIntegration;

public class CreateGmailIntegrationCommandValidator : AbstractValidator<CreateGmailIntegrationCommand>
{
    public CreateGmailIntegrationCommandValidator()
    {
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.State).NotEmpty();
    }
}
