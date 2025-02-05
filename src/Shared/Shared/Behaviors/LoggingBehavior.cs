namespace Shared.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "[START] Handle request={0} - response={1} - RequestData={2}",
            typeof(TRequest).Name,
            typeof(TResponse).Name,
            request);

        var timer = new Stopwatch();
        timer.Start();

        var response = await next();
        timer.Stop();

        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 3)
        {
            _logger.LogWarning("[Performance] The request {Request} took {TimeTaken} seconds", typeof(TRequest).Name, timeTaken.Seconds);
        }

        _logger.LogInformation(
            "[END] Handle request={Request} with response={Response}",
            typeof(TRequest).Name,
            typeof(TResponse).Name
        );

        return response;
    }
}
