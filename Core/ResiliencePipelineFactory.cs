using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Polly;
using Polly.Retry;
using Polly.Timeout;
using EBayFinancesApi.Core.Configuration;
using EBayFinancesApi.Core.Request;

namespace EBayFinancesApi.Core;

internal sealed class ResiliencePipelineFactory
{
    private readonly ResiliencePipeline<HttpResponseMessage> _pipeline;

    private static readonly ThreadLocal<Random> Jitterer = new(() => new Random());

    public ResiliencePipelineFactory(RetryOptions options)
    {
        _pipeline = CreateResiliencePipeline(options);
    }

    public ResiliencePipeline<HttpResponseMessage> Create(IRequest request) =>
        request.CanRetry ? _pipeline : ResiliencePipeline<HttpResponseMessage>.Empty;

    private static ResiliencePipeline<HttpResponseMessage> CreateResiliencePipeline(RetryOptions options)
    {
        var builder = new ResiliencePipelineBuilder<HttpResponseMessage>()
            .AddRetry(new RetryStrategyOptions<HttpResponseMessage>
            {
                ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                    .Handle<HttpRequestException>()
                    .HandleResult(msg =>
                        options.StatusCodesToRetry.Contains(msg.StatusCode) &&
                        msg.RequestMessage?.Method is { } method &&
                        options.HttpMethodsToRetry.Contains(method)),
                MaxRetryAttempts = options.MaxRetries,
                DelayGenerator = args =>
                {
                    var retryAttempt = args.AttemptNumber + 1;
                    double delayMs;

                    if (options.UseExponentialBackoff)
                    {
                        delayMs = options.Delay.TotalMilliseconds *
                                  Math.Pow(options.BackOffFactor, retryAttempt - 1);
                    }
                    else
                    {
                        delayMs = options.Delay.TotalMilliseconds;
                    }

                    var jitterMs = options.MaxJitter <= TimeSpan.Zero
                        ? 0
                        : Jitterer.Value!.Next((int)options.MaxJitter.TotalMilliseconds);

                    return new ValueTask<TimeSpan?>(TimeSpan.FromMilliseconds(delayMs + jitterMs));
                },
                OnRetry = args =>
                {
                    if (options.OnRetry is { } callback)
                    {
                        RetryReason reason = args.Outcome.Exception is { } ex
                            ? new RetryReason.Failure(ex)
                            : new RetryReason.Status(args.Outcome.Result!.StatusCode);

                        callback(new RetryAttempt
                        {
                            AttemptNumber = args.AttemptNumber + 1,
                            Delay = args.RetryDelay,
                            Reason = reason,
                        });
                    }

                    // Dispose the failed-attempt response to prevent socket leaks.
                    args.Outcome.Result?.Dispose();
                    return default;
                }
            });

        // Retry outer, Timeout inner → per-attempt timeout, not cumulative.
        if (options.Timeout is { } timeout)
            builder.AddTimeout(new TimeoutStrategyOptions { Timeout = timeout });

        return builder.Build();
    }
}
