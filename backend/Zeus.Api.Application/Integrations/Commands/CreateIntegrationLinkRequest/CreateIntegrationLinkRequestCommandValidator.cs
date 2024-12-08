using FluentValidation;

namespace Zeus.Api.Application.Integrations.Commands.CreateIntegrationLinkRequest;

public class CreateIntegrationLinkRequestCommandValidator : AbstractValidator<CreateIntegrationLinkRequestCommand>
{
    public CreateIntegrationLinkRequestCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Type).IsInEnum();
    }
}
