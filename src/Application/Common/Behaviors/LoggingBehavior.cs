using Application.Common.Attributes;
using Application.Common.Services;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviors;

[RegisterOpenBehavior(typeof(LoggingBehavior<,>))]
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse> 
    where TResponse : IErrorOr
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    private readonly IDateTimeProvider _dateTimeProvider;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger, IDateTimeProvider dateTimeProvider)
    {
        _logger = logger;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{@UtcNow} Starting {@RequestName}", _dateTimeProvider.UtcNow, typeof(TRequest).Name);

        var result = await next();

        if(result.IsError)
        {
            var errorMessage = "{@UtcNow} {@RequestName} failed, Errors: ";
            foreach (var error in result.Errors!)
            {
                errorMessage += $"{error.Description}, ";
            }
            _logger.LogError(errorMessage, _dateTimeProvider.UtcNow, typeof(TRequest).Name);
        }

        _logger.LogInformation("{@UtcNow} Completed {@RequestName}", _dateTimeProvider.UtcNow, typeof(TRequest).Name);

        return result;
    }
}
