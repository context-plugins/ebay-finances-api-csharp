using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EBayFinancesApi.Core;
using EBayFinancesApi.Core.Exceptions;
using EBayFinancesApi.Core.Models;
using EBayFinancesApi.Core.Request;
using EBayFinancesApi.Core.Response;
using EBayFinancesApi.Errors;
using EBayFinancesApi.Models;

namespace EBayFinancesApi.Api;

public sealed class PayoutModel
{
    private readonly RawClient _rawClient;
    private readonly Server _server;
    private readonly AuthSchemes _auth;

    internal PayoutModel(RawClient rawClient, Server server, AuthSchemes auth)
    {
        _rawClient = rawClient;
        _server = server;
        _auth = auth;
    }

    /// <summary>
    /// &lt;div class="msgbox_important"&gt;&lt;p class="msgbox_importantInDiv" data-mc-autonum="&amp;lt;b&amp;gt;&amp;lt;span style=&amp;quot;color: #dd1e31;&amp;quot; class=&amp;quot;mcFormatColor&amp;quot;&amp;gt;Important! &amp;lt;/span&amp;gt;&amp;lt;/b&amp;gt;"&gt;&lt;span class="autonumber"&gt;&lt;span&gt;&lt;b&gt;&lt;span style="color: #dd1e31;" class="mcFormatColor"&gt;Important!&lt;/span&gt;&lt;/b&gt;&lt;/span&gt;&lt;/span&gt; Due to EU &amp;amp; UK Payments regulatory requirements, an additional security verification via Digital Signatures is required for certain API calls that are made on behalf of EU/UK sellers, including all &lt;b&gt;Finances API&lt;/b&gt; methods. Please refer to &lt;a href="/develop/guides/digital-signatures-for-apis " target="_blank"&gt;Digital Signatures for APIs&lt;/a&gt; to learn more on the impacted APIs and the process to create signatures to be included in the HTTP payload.&lt;/p&gt;&lt;/div&gt;&lt;br&gt;This method retrieves details on a specific seller payout. The unique identfier of the payout is passed in as a path parameter at the end of the call URI. &lt;br&gt;&lt;br&gt;The &lt;b&gt;getPayouts&lt;/b&gt; method can be used to retrieve the unique identifier of a payout, or the user can check Seller Hub.
    /// </summary>
    /// <param name="payoutId">The unique identfier of the payout is passed in as a path parameter at the end of the call URI. &lt;br&gt;&lt;br&gt;The &lt;b&gt;getPayouts&lt;/b&gt; method can be used to retrieve the unique identifier of a payout, or the user can check Seller Hub to get the payout ID.</param>
    /// <param name="xEbayCMarketplaceId">This header identifies the seller's eBay marketplace. It is required for all marketplaces outside of the US. See &lt;a href="/api-docs/static/rest-request-components.html#marketpl " target="_blank "&gt;HTTP request headers&lt;/a&gt; for the marketplace ID values.</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>A <see cref="Task{TResult}"/> of <see cref="Payout"/> instance.</returns>
    /// <exception cref="SdkException{TResult}"> of <see cref="GetPayoutError"/> when the server returns an error response.</exception>
    public Task<Payout> GetPayout(string payoutId, string? xEbayCMarketplaceId, CancellationToken ct = default) =>
        _rawClient.Execute(_server.Default("/payout/{payout_Id}"),
            [new TemplateParam("payout_Id", payoutId)],
            [],
            [new HeaderParam("X-EBAY-C-MARKETPLACE-ID", xEbayCMarketplaceId)],
            HttpMethod.Get,
            EmptyBody.Instance,
            JsonResponse.Create<Payout>(),
            GetPayoutErrorResponse.Instance,
            [_auth.ApiAuth],
            ct);

    /// <summary>
    /// &lt;div class="msgbox_important"&gt;&lt;p class="msgbox_importantInDiv" data-mc-autonum="&amp;lt;b&amp;gt;&amp;lt;span style=&amp;quot;color: #dd1e31;&amp;quot; class=&amp;quot;mcFormatColor&amp;quot;&amp;gt;Important! &amp;lt;/span&amp;gt;&amp;lt;/b&amp;gt;"&gt;&lt;span class="autonumber"&gt;&lt;span&gt;&lt;b&gt;&lt;span style="color: #dd1e31;" class="mcFormatColor"&gt;Important!&lt;/span&gt;&lt;/b&gt;&lt;/span&gt;&lt;/span&gt; Due to EU &amp;amp; UK Payments regulatory requirements, an additional security verification via Digital Signatures is required for certain API calls that are made on behalf of EU/UK sellers, including all &lt;b&gt;Finances API&lt;/b&gt; methods. Please refer to &lt;a href="/develop/guides/digital-signatures-for-apis " target="_blank"&gt;Digital Signatures for APIs&lt;/a&gt; to learn more on the impacted APIs and the process to create signatures to be included in the HTTP payload.&lt;/p&gt;&lt;/div&gt;&lt;br&gt;This method is used to retrieve cumulative values for payouts in a particular state, or all states. The metadata in the response includes total payouts, the total number of monetary transactions (sales, refunds, credits) associated with those payouts, and the total dollar value of all payouts.&lt;br&gt;&lt;br&gt;If the &lt;b&gt;filter&lt;/b&gt; query parameter is used to filter by payout status, only one payout status value may be used. If the &lt;b&gt;filter&lt;/b&gt; query parameter is not used to filter by a specific payout status, cumulative values for payouts in all states are returned.&lt;br&gt;&lt;br&gt;The user can also use the &lt;b&gt;filter&lt;/b&gt; query parameter to specify a date range, and then only payouts that were processed within that date range are considered.
    /// </summary>
    /// <param name="filter">The two filter types that can be used here are discussed below. One or both of these filter types can be used. If none of these filters are used, the data returned in the response will reflect payouts, in all states, processed within the last 90 days. &lt;ul&gt;&lt;li&gt;&lt;b&gt;payoutDate&lt;/b&gt;: consider payouts processed within a specific range of dates. The date format to use is &lt;code&gt;YYYY-MM-DDTHH:MM:SS.SSSZ&lt;/code&gt;. Below is the proper syntax to use if filtering by a date range: &lt;br&gt;&lt;br&gt;&lt;code&gt;https://apiz.ebay.com/sell/finances/v1/payout_summary?filter=payoutDate:[2018-12-17T00:00:01.000Z..2018-12-24T00:00:01.000Z]&lt;/code&gt;&lt;br&gt;&lt;br&gt;Alternatively, the user could omit the ending date, and the date range would include the starting date and up to 90 days past that date, or the current date if the starting date is less than 90 days in the past.&lt;/li&gt; &lt;li&gt;&lt;b&gt;payoutStatus&lt;/b&gt;: consider only the payouts in a particular state. Only one payout state can be specified with this filter. The supported &lt;b&gt;payoutStatus&lt;/b&gt; values are as follows:&lt;ul&gt;&lt;li&gt;&lt;code&gt;INITIATED&lt;/code&gt;: search for payouts that have been initiated but not processed.&lt;/li&gt;&lt;li&gt;&lt;code&gt;SUCCEEDED&lt;/code&gt;: consider only successful payouts.&lt;/li&gt;&lt;li&gt;&lt;code&gt;RETRYABLE_FAILED&lt;/code&gt;: consider only payouts that failed, but ones which will be tried again.&lt;/li&gt;&lt;li&gt;&lt;code&gt;TERMINAL_FAILED&lt;/code&gt;: consider only payouts that failed, and ones that will not be tried again.&lt;/li&gt;&lt;li&gt; &lt;code&gt;REVERSED&lt;/code&gt;: consider only payouts that were reversed. &lt;/li&gt;&lt;/ul&gt;Below is the proper syntax to use if filtering by payout status: &lt;br&gt;&lt;br&gt;&lt;code&gt;https://apiz.ebay.com/sell/finances/v1/payout_summary?filter=payoutStatus:{SUCCEEDED}&lt;/code&gt;&lt;/ul&gt;&lt;br&gt;If both the &lt;b&gt;payoutDate&lt;/b&gt; and &lt;b&gt;payoutStatus&lt;/b&gt; filters are used, only the payouts that satisfy both criteria are considered in the results. For implementation help, refer to eBay API documentation at https://developer.ebay.com/api-docs/sell/finances/types/cos:FilterField</param>
    /// <param name="xEbayCMarketplaceId">This header identifies the seller's eBay marketplace. It is required for all marketplaces outside of the US. See &lt;a href="/api-docs/static/rest-request-components.html#marketpl " target="_blank "&gt;HTTP request headers&lt;/a&gt; for the marketplace ID values.</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>A <see cref="Task{TResult}"/> of <see cref="PayoutSummaryResponse"/> instance.</returns>
    /// <exception cref="SdkException{TResult}"> of <see cref="GetPayoutSummaryError"/> when the server returns an error response.</exception>
    public Task<PayoutSummaryResponse> GetPayoutSummary(string? filter,
        string? xEbayCMarketplaceId,
        CancellationToken ct = default) =>
        _rawClient.Execute(_server.Default("/payout_summary"),
            [],
            [new Param("filter", filter)],
            [new HeaderParam("X-EBAY-C-MARKETPLACE-ID", xEbayCMarketplaceId)],
            HttpMethod.Get,
            EmptyBody.Instance,
            JsonResponse.Create<PayoutSummaryResponse>(),
            GetPayoutSummaryErrorResponse.Instance,
            [_auth.ApiAuth],
            ct);

    /// <summary>
    /// &lt;div class="msgbox_important"&gt;&lt;p class="msgbox_importantInDiv" data-mc-autonum="&amp;lt;b&amp;gt;&amp;lt;span style=&amp;quot;color: #dd1e31;&amp;quot; class=&amp;quot;mcFormatColor&amp;quot;&amp;gt;Important! &amp;lt;/span&amp;gt;&amp;lt;/b&amp;gt;"&gt;&lt;span class="autonumber"&gt;&lt;span&gt;&lt;b&gt;&lt;span style="color: #dd1e31;" class="mcFormatColor"&gt;Important!&lt;/span&gt;&lt;/b&gt;&lt;/span&gt;&lt;/span&gt; Due to EU &amp;amp; UK Payments regulatory requirements, an additional security verification via Digital Signatures is required for certain API calls that are made on behalf of EU/UK sellers, including all &lt;b&gt;Finances API&lt;/b&gt; methods. Please refer to &lt;a href="/develop/guides/digital-signatures-for-apis " target="_blank"&gt;Digital Signatures for APIs&lt;/a&gt; to learn more on the impacted APIs and the process to create signatures to be included in the HTTP payload.&lt;/p&gt;&lt;/div&gt;&lt;br&gt;This method is used to retrieve the details of one or more seller payouts. By using the &lt;b&gt;filter&lt;/b&gt; query parameter, users can retrieve payouts processed within a specific date range, and/or they can retrieve payouts in a specific state.&lt;br&gt;&lt;br&gt;There are also pagination and sort query parameters that allow users to control the payouts that are returned in the response.&lt;br&gt;&lt;br&gt;If no payouts match the input criteria, an empty payload is returned.
    /// </summary>
    /// <param name="filter">The three filter types that can be used here are discussed below. If none of these filters are used, all recent payouts in all states are returned:&lt;ul&gt;&lt;li&gt;&lt;b&gt;payoutDate&lt;/b&gt;: search for payouts within a specific range of dates. The date format to use is &lt;code&gt;YYYY-MM-DDTHH:MM:SS.SSSZ&lt;/code&gt;. Below is the proper syntax to use if filtering by a date range: &lt;br&gt;&lt;br&gt;&lt;code&gt;https://apiz.ebay.com/sell/finances/v1/payout?filter=payoutDate:[2018-12-17T00:00:01.000Z..2018-12-24T00:00:01.000Z]&lt;/code&gt;&lt;br&gt;&lt;br&gt;Alternatively, the user could omit the ending date, and the date range would include the starting date and up to 90 days past that date, or the current date if the starting date is less than 90 days in the past.&lt;/li&gt;&lt;li&gt;&lt;b&gt;lastAttemptedPayoutDate&lt;/b&gt;: search for attempted payouts that failed within a specific range of dates. In order to use this filter, the &lt;b&gt;payoutStatus&lt;/b&gt; filter must also be used and its value must be set to &lt;code&gt;RETRYABLE_FAILED&lt;/code&gt;. The date format to use is &lt;code&gt;YYYY-MM-DDTHH:MM:SS.SSSZ&lt;/code&gt;. The same syntax used for the &lt;b&gt;payoutDate&lt;/b&gt; filter is also used for the &lt;b&gt;lastAttemptedPayoutDate&lt;/b&gt; filter. &lt;br&gt;&lt;br&gt;This filter is only applicable (and will return results) if one or more seller payouts have failed, but are retryable.&lt;/li&gt; &lt;li&gt;&lt;b&gt;payoutStatus&lt;/b&gt;: search for payouts in a particular state. Only one payout state can be specified with this filter. The supported &lt;b&gt;payoutStatus&lt;/b&gt; values are as follows:&lt;ul&gt;&lt;li&gt;&lt;code&gt;INITIATED&lt;/code&gt;: search for payouts that have been initiated but not processed.&lt;/li&gt;&lt;li&gt;&lt;code&gt;SUCCEEDED&lt;/code&gt;: search for successful payouts.&lt;/li&gt;&lt;li&gt;&lt;code&gt;RETRYABLE_FAILED&lt;/code&gt;: search for payouts that failed, but ones which will be tried again. This value must be used if filtering by &lt;b&gt;lastAttemptedPayoutDate&lt;/b&gt;. &lt;/li&gt;&lt;li&gt;&lt;code&gt;TERMINAL_FAILED&lt;/code&gt;: search for payouts that failed, and ones that will not be tried again.&lt;/li&gt;&lt;li&gt; &lt;code&gt;REVERSED&lt;/code&gt;: search for payouts that were reversed. &lt;/li&gt;&lt;/ul&gt;Below is the proper syntax to use if filtering by payout status: &lt;br&gt;&lt;br&gt;&lt;code&gt;https://apiz.ebay.com/sell/finances/v1/payout?filter=payoutStatus:{SUCCEEDED}&lt;/code&gt;&lt;/ul&gt;&lt;br&gt;If both the &lt;b&gt;payoutDate&lt;/b&gt; and &lt;b&gt;payoutStatus&lt;/b&gt; filters are used, payouts must satisfy both criteria to be returned. For implementation help, refer to eBay API documentation at https://developer.ebay.com/api-docs/sell/finances/types/cos:FilterField</param>
    /// <param name="limit">The number of payouts to return per page of the result set. Use this parameter in conjunction with the &lt;b&gt;offset&lt;/b&gt; parameter to control the pagination of the output. &lt;br&gt;&lt;br&gt; For example, if &lt;b&gt;offset&lt;/b&gt; is set to &lt;code&gt;10&lt;/code&gt; and &lt;b&gt;limit&lt;/b&gt; is set to &lt;code&gt;10&lt;/code&gt;, the method retrieves payouts 11 thru 20 from the result set. &lt;br&gt;&lt;br&gt; &lt;span class="tablenote"&gt;&lt;strong&gt;Note:&lt;/strong&gt; This feature employs a zero-based list, where the first payout in the results set has an offset value of &lt;code&gt;0&lt;/code&gt;. &lt;/span&gt; &lt;br&gt;&lt;br&gt; &lt;b&gt;Maximum:&lt;/b&gt; &lt;code&gt;200&lt;/code&gt; &lt;br&gt; &lt;b&gt;Default:&lt;/b&gt; &lt;code&gt;20&lt;/code&gt;</param>
    /// <param name="offset">This integer value indicates the actual position that the first payout returned on the current page has in the results set. So, if you wanted to view the 11th payout of the result set, you would set the &lt;strong&gt;offset&lt;/strong&gt; value in the request to &lt;code&gt;10&lt;/code&gt;. &lt;br&gt;&lt;br&gt;In the request, you can use the &lt;b&gt;offset&lt;/b&gt; parameter in conjunction with the &lt;b&gt;limit&lt;/b&gt; parameter to control the pagination of the output. For example, if &lt;b&gt;offset&lt;/b&gt; is set to &lt;code&gt;30&lt;/code&gt; and &lt;b&gt;limit&lt;/b&gt; is set to &lt;code&gt;10&lt;/code&gt;, the method retrieves payouts 31 thru 40 from the resulting collection of payouts. &lt;br&gt;&lt;br&gt; &lt;span class="tablenote"&gt;&lt;strong&gt;Note:&lt;/strong&gt; This feature employs a zero-based list, where the first payout in the results set has an offset value of &lt;code&gt;0&lt;/code&gt;.&lt;/span&gt;&lt;br&gt;&lt;br&gt;&lt;b&gt;Default:&lt;/b&gt; &lt;code&gt;0&lt;/code&gt; (zero)</param>
    /// <param name="sort">By default, payouts or failed payouts that match the input criteria are sorted in descending order according to the payout date/last attempted payout date (i.e., most recent payouts returned first).&lt;br&gt;&lt;br&gt;To view payouts in ascending order instead (i.e., oldest payouts/attempted payouts first,) you would include the &lt;b&gt;sort&lt;/b&gt; query parameter, and then set the value of its &lt;b&gt;field&lt;/b&gt; parameter to &lt;code&gt;payoutDate&lt;/code&gt; or &lt;code&gt;lastAttemptedPayoutDate&lt;/code&gt; (if searching for failed, retryable payouts). Below is the proper syntax to use if filtering by a payout date range in ascending order:&lt;br&gt;&lt;br&gt;&lt;code&gt;https://apiz.ebay.com/sell/finances/v1/payout?filter=payoutDate:[2018-12-17T00:00:01.000Z..2018-12-24T00:00:01.000Z]&amp;sort=payoutDate&lt;/code&gt;&lt;br&gt;&lt;br&gt;Payouts can only be sorted according to payout date, and can not be sorted by payout status. For implementation help, refer to eBay API documentation at https://developer.ebay.com/api-docs/sell/finances/types/cos:SortField</param>
    /// <param name="xEbayCMarketplaceId">This header identifies the seller's eBay marketplace. It is required for all marketplaces outside of the US. See &lt;a href="/api-docs/static/rest-request-components.html#marketpl " target="_blank "&gt;HTTP request headers&lt;/a&gt; for the marketplace ID values.</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>A <see cref="Task{TResult}"/> of <see cref="Payouts"/> instance.</returns>
    /// <exception cref="SdkException{TResult}"> of <see cref="GetPayoutsError"/> when the server returns an error response.</exception>
    public Task<Payouts> GetPayouts(string? filter,
        string? limit,
        string? offset,
        string? sort,
        string? xEbayCMarketplaceId,
        CancellationToken ct = default) =>
        _rawClient.Execute(_server.Default("/payout"),
            [],
            [new Param("filter", filter),
                new Param("limit", limit),
                new Param("offset", offset),
                new Param("sort", sort)],
            [new HeaderParam("X-EBAY-C-MARKETPLACE-ID", xEbayCMarketplaceId)],
            HttpMethod.Get,
            EmptyBody.Instance,
            JsonResponse.Create<Payouts>(),
            GetPayoutsErrorResponse.Instance,
            [_auth.ApiAuth],
            ct);
}
