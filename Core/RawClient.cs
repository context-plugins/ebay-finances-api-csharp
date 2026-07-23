using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using EBayFinancesApi.Core.Authentication;
using EBayFinancesApi.Core.ErrorResponse;
using EBayFinancesApi.Core.Extensions;
using EBayFinancesApi.Core.Models;
using EBayFinancesApi.Core.Pagination;
using EBayFinancesApi.Core.Pagination.States;
using EBayFinancesApi.Core.Request;
using EBayFinancesApi.Core.Response;

namespace EBayFinancesApi.Core;

internal sealed class RawClient
{
    private delegate Task<ApiResult<TResponse, TError>> ApiCallHandler<TResponse, TError>(
        ApiRequest request,
        ApiResponse<TResponse, TError> response,
        CancellationToken cancellationToken);

    private readonly HeadersFactory _headerFactory;
    private readonly HttpClient _httpClient;
    private readonly HttpStatusPolicy _statusPolicy;
    private readonly UriFactory _uriFactory;
    private readonly ResiliencePipelineFactory _resiliencePipelineFactory;

    public RawClient(HttpClient httpClient, UriFactory uriFactory,
        HttpStatusPolicy statusPolicy, HeadersFactory headerFactory,
        ResiliencePipelineFactory resiliencePipelineFactory)
    {
        _httpClient = httpClient;
        _uriFactory = uriFactory;
        _statusPolicy = statusPolicy;
        _headerFactory = headerFactory;
        _resiliencePipelineFactory = resiliencePipelineFactory;
    }

    public Task<ApiResult<TResponse, TError>> ExecuteResult<TResponse, TError>(
        UrlTemplate urlTemplate,
        IReadOnlyCollection<TemplateParam> templateParameters,
        IReadOnlyCollection<Param> queryParameters,
        IReadOnlyCollection<HeaderParam> headerParameters,
        HttpMethod httpMethod,
        IRequest request,
        IResponse<TResponse> response,
        IErrorResponse<TError> errorResponseDeserializer,
        IReadOnlyList<IAuthScheme> authPolicies,
        CancellationToken cancellationToken) =>
        ExecuteResult(
            new ApiRequest(
                urlTemplate,
                templateParameters,
                queryParameters,
                headerParameters,
                httpMethod,
                request,
                authPolicies),
            ApiResponse.Create(response, errorResponseDeserializer),
            cancellationToken);

    public async Task<TResponse> Execute<TResponse, TError>(
        UrlTemplate urlTemplate,
        IReadOnlyCollection<TemplateParam> templateParameters,
        IReadOnlyCollection<Param> queryParameters,
        IReadOnlyCollection<HeaderParam> headerParameters,
        HttpMethod httpMethod,
        IRequest request,
        IResponse<TResponse> response,
        IErrorResponse<TError> errorResponseDeserializer,
        IReadOnlyList<IAuthScheme> authPolicies,
        CancellationToken cancellationToken) =>
        (await ExecuteResult(
            urlTemplate,
            templateParameters,
            queryParameters,
            headerParameters,
            httpMethod,
            request,
            response,
            errorResponseDeserializer,
            authPolicies,
            cancellationToken).ConfigureAwait(false)).GetResponseOrThrow();

    public Pageable<TResponse, TItem> ExecutePaged<TResponse, TState, TItem, TError>(
        TState initialState,
        Func<TState, ApiRequest> requestFactory,
        Func<TResponse, IReadOnlyList<TItem>> itemsSelector,
        ApiResponse<TResponse, TError> response,
        CancellationToken cancellationToken)
        where TState : IPageState<TResponse, TState>
    {
        var pages = ExecutePagedResult(initialState, requestFactory, itemsSelector, response,
            cancellationToken);
        return new Pageable<TResponse, TItem>(ThrowOnError(pages.AsPages(cancellationToken)), itemsSelector);

        static async IAsyncEnumerable<TResponse> ThrowOnError(
            IAsyncEnumerable<ApiResult<TResponse, TError>> pages,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            await foreach (var result in pages.WithCancellation(cancellationToken).ConfigureAwait(false))
                yield return result.GetResponseOrThrow();
        }
    }

    public Pageable<ApiResult<TResponse, TError>, TItem> ExecutePagedResult<TResponse, TState, TItem, TError>(
        TState initialState,
        Func<TState, ApiRequest> requestFactory,
        Func<TResponse, IReadOnlyList<TItem>> itemsSelector,
        ApiResponse<TResponse, TError> response,
        CancellationToken cancellationToken)
        where TState : IPageState<TResponse, TState>
    {
        var pages = Paginate(initialState, requestFactory, response, ExecuteResult, cancellationToken);

        return new Pageable<ApiResult<TResponse, TError>, TItem>(pages, apiResult =>
            apiResult.TryGetResponse(out var page) ? itemsSelector(page) : []);

        static async IAsyncEnumerable<ApiResult<TResponse, TError>> Paginate(
            TState initialState,
            Func<TState, ApiRequest> requestFactory,
            ApiResponse<TResponse, TError> response,
            ApiCallHandler<TResponse, TError> execute,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var state = initialState;

            while (true)
            {
                var result = await execute(requestFactory(state), response, cancellationToken)
                    .ConfigureAwait(false);

                yield return result;

                if (!result.TryGetResponse(out var page))
                    yield break;

                var next = state.Next(page, result.Headers);
                if (next == null)
                    yield break;

                state = next;
            }
        }
    }

    private async Task<ApiResult<TResponse, TError>> ExecuteResult<TResponse, TError>(
        ApiRequest request,
        ApiResponse<TResponse, TError> response,
        CancellationToken cancellationToken)
    {
        var uri = _uriFactory.Create(request.UrlTemplate, request.QueryParameters, request.TemplateParameters);
        var headers = _headerFactory.Create(request.HeaderParameters);

        var pipeline = _resiliencePipelineFactory.Create(request.Request);

        // The response is not disposed here: on success its lifetime is owned by IResponse.Map
        // (buffered responses dispose it immediately, streaming ones hand it to their iterator);
        // the error path below owns disposal explicitly.
        var httpResponseMessage = await pipeline.ExecuteAsync(async ct =>
        {
            // Dispose only the content: HttpRequestMessage.Dispose does nothing more, and the
            // retry predicate still reads Method off this request after the attempt returns.
            var httpRequest = new HttpRequestMessage(request.HttpMethod, uri);
            try
            {
                httpRequest.Content = request.Request.Get();
                httpRequest.Headers.AddRange(headers);
                await request.AuthPolicies.Apply(httpRequest, ct).ConfigureAwait(false);
                return await _httpClient.SendAsync(httpRequest, HttpCompletionOption.ResponseHeadersRead, ct)
                    .ConfigureAwait(false);
            }
            finally
            {
                httpRequest.Content?.Dispose();
            }
        }, cancellationToken).ConfigureAwait(false);

        // Capture before IResponse.Map runs — buffered responses dispose the message inside Map.
        var statusCode = httpResponseMessage.StatusCode;
        var responseHeaders = httpResponseMessage.Headers;

        if (_statusPolicy.IsSuccess(statusCode))
        {
            var successResponse =
                await response.Response.Map(httpResponseMessage, cancellationToken).ConfigureAwait(false);
            return ApiResult<TResponse, TError>.Success(successResponse, statusCode, responseHeaders);
        }

        using (httpResponseMessage)
        {
            if (_statusPolicy.IsUnauthorized(statusCode))
                request.AuthPolicies.InvalidateRevocable();

            var errorResponse = await response.ErrorResponseDeserializer.Map(httpResponseMessage, cancellationToken)
                .ConfigureAwait(false);

            return ApiResult<TResponse, TError>.Failure(errorResponse, statusCode, responseHeaders);
        }
    }
}
