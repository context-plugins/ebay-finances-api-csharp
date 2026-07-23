using System.Text.Json.Serialization;

namespace EBayFinancesApi.Models;

/// <summary>
/// This type is the base response type of the &lt;strong&gt;getPayoutSummary&lt;/strong&gt; method, and contains the total count of seller payouts (that match the input criteria), the total count of monetary transactions (order payment, buyer refunds, or seller credits) associated with those payouts, and the total value of those seller payouts.
/// </summary>
public record PayoutSummaryResponse
{
    /// <summary>
    /// This type is used to express the dollar value and currency used for any transaction retrieved with the &lt;strong&gt;Finances API&lt;/strong&gt;, including an order total, a seller payout, a buyer refund, or a seller credit.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("amount")]
    public Amount? Amount { get; init; }

    /// <summary>
    /// This integer value indicates the total count of payouts to the seller that match the input criteria. This field is always returned, even if there are no payouts that match the input criteria (its value will show &lt;code&gt;0&lt;/code&gt;).
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("payoutCount")]
    public int? PayoutCount { get; init; }

    /// <summary>
    /// This integer value indicates the total count of monetary transactions (order payments, buyer refunds, and seller credits) associated with the payouts that match the input criteria. This field is always returned, even if there are no payouts that match the input criteria (its value will show &lt;code&gt;0&lt;/code&gt;). If there is at least one payout that matches the input criteria, the value in this field will be at least &lt;code&gt;1&lt;/code&gt;.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("transactionCount")]
    public int? TransactionCount { get; init; }
}
