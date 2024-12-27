using ErrorOr;

using FluentValidation;

using MediatR;

namespace Zeus.BuildingBlocks.Application.Behaviors;

public class ValidateBehavior<TMessage, TResponse> :
    IPipelineBehavior<TMessage, TResponse>
    where TMessage : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IValidator<TMessage>? _validator;

    public ValidateBehavior(IValidator<TMessage>? validator = null)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TMessage request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator is null)
        {
            return await next();
        }

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            return await next();
        }

        var errors = validationResult.Errors.ConvertAll(e => Error.Validation(e.PropertyName, e.ErrorMessage));
        return (dynamic) errors;
    }
}
