using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors;

public class LoggingBehaviour
<TRequest, TResponse> 
(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
: IPipelineBehavior<TRequest, TResponse>
where TRequest : notnull, IRequest<TResponse>
where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[Start] Handle request={Request} - Response={Response} - RequestData={RequestData}",
        typeof(TRequest).Name,
        typeof(TResponse).Name, request);

        var timer = new Stopwatch();
        timer.Start();

        var response = await next();

        timer.Stop();

        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 3)
            logger.LogWarning("[Performance] The request {Request} took {TimeTaken} seconds"
               , typeof(TRequest).Name, timeTaken.Seconds);

        logger.LogInformation("[END] Handled {Request} with {Response}",
        typeof(TRequest).Name,
        typeof(TResponse));

        return response;
    }
}