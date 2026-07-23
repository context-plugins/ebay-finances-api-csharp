using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.ServerSentEvents;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using EBayFinancesApi.Core.Exceptions;

namespace EBayFinancesApi.Core.Response;

internal static class SseFrameReader
{
    public static async IAsyncEnumerable<byte[]> EnumerateFrames(
        HttpResponseMessage response,
        byte[]? sentinelBytes,
        TimeSpan? idleTimeout,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        using (response)
        {
#if NET6_0_OR_GREATER
            var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
#else
            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
#endif

            var parser = SseParser.Create(stream, static (_, data) => data.ToArray());

            if (idleTimeout is not { } idleWindow)
            {
                await foreach (var item in parser.EnumerateAsync(cancellationToken).ConfigureAwait(false))
                {
                    var frame = item.Data;
                    if (sentinelBytes is not null && frame.AsSpan().SequenceEqual(sentinelBytes))
                        yield break;

                    yield return frame;
                }

                yield break;
            }

            using var frameCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            var enumerator = parser.EnumerateAsync(frameCts.Token).GetAsyncEnumerator();
            try
            {
                while (true)
                {
                    var moveNext = enumerator.MoveNextAsync();

                    bool hasNext;
                    if (moveNext.IsCompletedSuccessfully)
                    {
                        hasNext = moveNext.Result;
                    }
                    else
                    {
                        var moveNextTask = moveNext.AsTask();
                        using var timerCts = new CancellationTokenSource();
                        var idleDelay = Task.Delay(idleWindow, timerCts.Token);

                        if (await Task.WhenAny(moveNextTask, idleDelay).ConfigureAwait(false) == idleDelay)
                        {
                            frameCts.Cancel();
                            try
                            {
                                await moveNextTask.ConfigureAwait(false);
                            }
                            catch (Exception)
                            {
                                // The read we just cancelled — its outcome is irrelevant; we are
                                // reporting the timeout instead.
                            }

                            throw new SseTimeoutException(idleWindow);
                        }

                        timerCts.Cancel();
                        hasNext = await moveNextTask.ConfigureAwait(false);
                    }

                    if (!hasNext)
                        yield break;

                    var data = enumerator.Current.Data;
                    if (sentinelBytes is not null && data.AsSpan().SequenceEqual(sentinelBytes))
                        yield break;

                    yield return data;
                }
            }
            finally
            {
                await enumerator.DisposeAsync().ConfigureAwait(false);
            }
        }
    }
}
