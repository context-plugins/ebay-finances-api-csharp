using System.Text.Json.Serialization;

namespace EBayFinancesApi.Models;

/// <summary>
/// This type is used by the &lt;b&gt;balanceAdjustment&lt;/b&gt; container, which shows the seller payout balance that will be applied toward the charges outlined in the &lt;b&gt;charges&lt;/b&gt; array.
/// </summary>
public record BalanceAdjustment
{
    /// <summary>
    /// This type is used to express the dollar value and currency used for any transaction retrieved with the &lt;strong&gt;Finances API&lt;/strong&gt;, including an order total, a seller payout, a buyer refund, or a seller credit.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("adjustmentAmount")]
    public Amount? AdjustmentAmount { get; init; }

    /// <summary>
    /// The enumeration value returned here indicates if the charge is a &lt;code&gt;DEBIT&lt;/code&gt; or a &lt;code&gt;CREDIT&lt;/code&gt; to the seller. Generally, all transfer transaction types are going to be &lt;code&gt;DEBIT&lt;/code&gt;, since the money is being tranferred from the seller to eBay. For implementation help, refer to &lt;a href='https://developer.ebay.com/api-docs/sell/finances/types/pay:BookingEntryEnum'&gt;eBay API documentation&lt;/a&gt;
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("adjustmentType")]
    public string? AdjustmentType { get; init; }
}
