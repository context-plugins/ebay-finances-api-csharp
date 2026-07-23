using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EBayFinancesApi.Models;

/// <summary>
/// This type is the base response type of the &lt;strong&gt;getPayouts&lt;/strong&gt; method, and contains an array of one or more payouts (that match the input criteria), the total count of payouts in the response, and various pagination data for the results set.
/// </summary>
public record Payouts
{
    /// <summary>
    /// The URI of the &lt;b&gt;getPayouts&lt;/b&gt; call request that produced the current page of the result set.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("href")]
    public string? Href { get; init; }

    /// <summary>
    /// The maximum number of payouts that may be returned per page of the result set. The &lt;strong&gt;limit&lt;/strong&gt; value can be passed in as a query parameter, or if omitted, its value defaults to &lt;code&gt;20&lt;/code&gt;. &lt;br&gt;&lt;br&gt;&lt;span class="tablenote"&gt;&lt;strong&gt;Note:&lt;/strong&gt; If this is the last or only page of the result set, the page may contain fewer payouts than the &lt;strong&gt;limit&lt;/strong&gt; value.  To determine the number of pages in a result set, divide the &lt;b&gt;total&lt;/b&gt; value (total number of payouts matching input criteria) by this &lt;strong&gt;limit&lt;/strong&gt; value, and then round up to the next integer. For example, if the &lt;b&gt;total&lt;/b&gt; value was &lt;code&gt;120&lt;/code&gt; (120 total payouts) and the &lt;strong&gt;limit&lt;/strong&gt; value was &lt;code&gt;50&lt;/code&gt; (show 50 payouts per page), the total number of pages in the result set is three, so the seller would have to make three separate &lt;strong&gt;getPayouts&lt;/strong&gt; calls to view all payouts matching the input criteria. &lt;/span&gt;&lt;br&gt;&lt;br&gt;&lt;b&gt;Maximum:&lt;/b&gt; &lt;code&gt;200&lt;/code&gt; &lt;br&gt; &lt;b&gt;Default:&lt;/b&gt; &lt;code&gt;20&lt;/code&gt;
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    /// <summary>
    /// The &lt;b&gt;getPayouts&lt;/b&gt; call URI to use if you wish to view the next page of the result set. &lt;br&gt;&lt;br&gt;This field is only returned if there is a next page of results to view based on the current input criteria.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("next")]
    public string? Next { get; init; }

    /// <summary>
    /// This integer value indicates the actual position that the first payout returned on the current page has in the results set. So, if you wanted to view the 11th payout of the result set, you would set the &lt;strong&gt;offset&lt;/strong&gt; value in the request to &lt;code&gt;10&lt;/code&gt;. &lt;br&gt;&lt;br&gt;In the request, you can use the &lt;b&gt;offset&lt;/b&gt; parameter in conjunction with the &lt;b&gt;limit&lt;/b&gt; parameter to control the pagination of the output. For example, if &lt;b&gt;offset&lt;/b&gt; is set to &lt;code&gt;30&lt;/code&gt; and &lt;b&gt;limit&lt;/b&gt; is set to &lt;code&gt;10&lt;/code&gt;, the call retrieves payouts 31 thru 40 from the resulting collection of payouts. &lt;br&gt;&lt;br&gt; &lt;span class="tablenote"&gt;&lt;strong&gt;Note:&lt;/strong&gt; This feature employs a zero-based list, where the first item in the list has an offset of &lt;code&gt;0&lt;/code&gt;.&lt;/span&gt;&lt;br&gt;&lt;br&gt;&lt;b&gt;Default:&lt;/b&gt; &lt;code&gt;0&lt;/code&gt; (zero)
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("offset")]
    public int? Offset { get; init; }

    /// <summary>
    /// An array of one or more payouts that match the input criteria. Details for each payout include the unique identifier of the payout, the status of the payout, the amount of the payout, and the number of monetary transactions associated with the payout.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("payouts")]
    public IReadOnlyList<Payout>? PayoutsValue { get; init; }

    /// <summary>
    /// The &lt;b&gt;getPayouts&lt;/b&gt; call URI to use if you wish to view the previous page of the result set. &lt;br&gt;&lt;br&gt;This field is only returned if there is a previous page of results to view based on the current input criteria.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("prev")]
    public string? Prev { get; init; }

    /// <summary>
    /// This integer value is the total number of payouts in the results set based on the current input criteria. Based on the total number of payouts that match the criteria, and on the &lt;strong&gt;limit&lt;/strong&gt; and &lt;strong&gt;offset&lt;/strong&gt; values, there may be additional pages in the results set.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("total")]
    public int? Total { get; init; }
}
