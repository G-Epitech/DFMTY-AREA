using FluentValidation;

namespace Zeus.Api.Application.Automations.Commands.CreateAutomation;

public class CreateAutomationCommandValidator : AbstractValidator<CreateAutomationCommand>
{
    public CreateAutomationCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}
