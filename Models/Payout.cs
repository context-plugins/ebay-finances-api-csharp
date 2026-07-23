using System.Text.Json.Serialization;

namespace EBayFinancesApi.Models;

/// <summary>
/// This type is used to express the details of one seller payout that is returned with the &lt;strong&gt;getPayout&lt;/strong&gt; or &lt;strong&gt;getPayouts&lt;/strong&gt; methods.
/// </summary>
public record Payout
{
    /// <summary>
    /// This type is used to express the dollar value and currency used for any transaction retrieved with the &lt;strong&gt;Finances API&lt;/strong&gt;, including an order total, a seller payout, a buyer refund, or a seller credit.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("amount")]
    public Amount? Amount { get; init; }

    /// <summary>
    /// This field contains additional information provided by the bank and passed on by the payment processor; e.g., the manner in which the transaction will appear on the seller's bank statement. The field is returned only when provided by the bank and processor.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("bankReference")]
    public string? BankReference { get; init; }

    /// <summary>
    /// This timestamp indicates the date/time when eBay last attempted to process a seller payout but it failed. This field is only returned if a seller payout fails, and the &lt;strong&gt;payoutStatus&lt;/strong&gt; value shows &lt;code&gt;RETRYABLE_FAILED&lt;/code&gt; or &lt;code&gt;TERMINAL_FAILED&lt;/code&gt;. A seller can filter on the &lt;b&gt;lastAttemptedPayoutDate&lt;/b&gt; in a &lt;b&gt;getPayouts&lt;/b&gt; request.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("lastAttemptedPayoutDate")]
    public string? LastAttemptedPayoutDate { get; init; }

    /// <summary>
    /// This timestamp indicates when the seller payout began processing. The following format is used: &lt;code&gt;YYYY-MM-DDTHH:MM:SS.SSSZ&lt;/code&gt;. For example, &lt;code&gt;2015-08-04T19:09:02.768Z&lt;/code&gt;. This field is still returned even if the payout was pending but failed (&lt;strong&gt;payoutStatus&lt;/strong&gt; value shows &lt;code&gt;RETRYABLE_FAILED&lt;/code&gt; or &lt;code&gt;TERMINAL_FAILED&lt;/code&gt;).
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("payoutDate")]
    public string? PayoutDate { get; init; }

    /// <summary>
    /// The unique identifier of the seller payout. This identifier is generated once eBay begins processing the payout to the seller's bank account.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("payoutId")]
    public string? PayoutId { get; init; }

    /// <summary>
    /// This type provides details about the seller's account that received (or is scheduled to receive) a payout.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("payoutInstrument")]
    public PayoutInstrument? PayoutInstrument { get; init; }

    /// <summary>
    /// This field contains information provided by upstream components, based on internal and external commitments. A typical message would contain the expected arrival time of the payout.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("payoutMemo")]
    public string? PayoutMemo { get; init; }

    /// <summary>
    /// This enumeration value indicates the current status of the seller payout. For a successful payout, the value returned will be &lt;code&gt;SUCCEEDED&lt;/code&gt;. See the &lt;strong&gt;PayoutStatusEnum&lt;/strong&gt; type for more details on each payout status value. For implementation help, refer to &lt;a href='https://developer.ebay.com/api-docs/sell/finances/types/pay:PayoutStatusEnum'&gt;eBay API documentation&lt;/a&gt;
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("payoutStatus")]
    public string? PayoutStatus { get; init; }

    /// <summary>
    /// This field provides more details about the current status of payout. The description returned here will correspond with enumeration value returned in the &lt;strong&gt;payoutStatus&lt;/strong&gt; field. The following shows what description text might appear based on the different &lt;strong&gt;payoutStatus&lt;/strong&gt; values:&lt;ul&gt;&lt;li&gt;&lt;code&gt;INITIATED&lt;/code&gt;: &lt;em&gt;Preparing to send&lt;/em&gt;&lt;/li&gt;&lt;li&gt;&lt;code&gt;SUCCEEDED&lt;/code&gt;: &lt;em&gt;Funds sent&lt;/em&gt;&lt;/li&gt;&lt;li&gt;&lt;code&gt;REVERSED&lt;/code&gt;: &lt;em&gt;Waiting to retry : Money rejected by seller's bank&lt;/em&gt;&lt;/li&gt;&lt;li&gt;&lt;code&gt;RETRYABLE_FAILED&lt;/code&gt;: &lt;em&gt;Waiting to retry&lt;/em&gt;&lt;/li&gt;&lt;li&gt;&lt;code&gt;TERMINAL_FAILED&lt;/code&gt;: &lt;em&gt;Payout failed&lt;/em&gt;&lt;/li&gt;&lt;/ul&gt;
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("payoutStatusDescription")]
    public string? PayoutStatusDescription { get; init; }

    /// <summary>
    /// This type is used to express the dollar value and currency used for any transaction retrieved with the &lt;strong&gt;Finances API&lt;/strong&gt;, including an order total, a seller payout, a buyer refund, or a seller credit.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("totalAmount")]
    public Amount? TotalAmount { get; init; }

    /// <summary>
    /// This type is used to express the dollar value and currency used for any transaction retrieved with the &lt;strong&gt;Finances API&lt;/strong&gt;, including an order total, a seller payout, a buyer refund, or a seller credit.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("totalFee")]
    public Amount? TotalFee { get; init; }

    /// <summary>
    /// This integer value indicates the number of monetary transactions (all orders, refunds, and credits, etc.) that have occurred with the corresponding payout. Its value should always be at least &lt;code&gt;1&lt;/code&gt;, since there is at least one order per seller payout.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("transactionCount")]
    public int? TransactionCount { get; init; }
}
