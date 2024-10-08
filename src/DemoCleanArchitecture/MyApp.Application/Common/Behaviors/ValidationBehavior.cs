﻿using MyApp.Application.Common.Exceptions;


namespace MyApp.Application.Common.Behaviors;


/// <summary>
///     Custom Validation Behavior middleware 
///     to generate MyValidationException object from the ModelState Errors.
/// </summary>
/// <typeparam name="TRequest">Request pipeline object.</typeparam>
/// <typeparam name="TResponse">Response pipeline object.</typeparam>
public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{

    private readonly IEnumerable<IValidator<TRequest>> _validators;


    public ValidationBehavior(
        IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }


    #region IPipelineBehavior members

    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults 
                = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {
                throw new MyValidationException(failures);
            }
        }

        return await next();
    }

    #endregion

}
