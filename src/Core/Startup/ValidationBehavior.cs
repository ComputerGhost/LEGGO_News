using FluentValidation;
using MediatR;

namespace Core.Startup;
internal class ValidationBehavior<TRequest, TResult> : IPipelineBehavior<TRequest, TResult>
    where TRequest : IBaseRequest
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResult> Handle(TRequest request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validators = _validators.Select(v => v.ValidateAsync(context));
        var results = await Task.WhenAll(validators);

        var errors = results
            .Where(result => !result.IsValid)
            .SelectMany(result => result.Errors);

        if (errors.Any())
        {
            throw new ValidationException(errors);
        }

        return await next();
    }
}
