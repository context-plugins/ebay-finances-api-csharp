using System.Text.Json.Serialization;

namespace EBayFinancesApi.Models;

/// <summary>
/// This type is used to display fees that are automatically deducted from seller payouts.
/// </summary>
public record Fee
{
    /// <summary>
    /// This type is used to express the dollar value and currency used for any transaction retrieved with the &lt;strong&gt;Finances API&lt;/strong&gt;, including an order total, a seller payout, a buyer refund, or a seller credit.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("amount")]
    public Amount? Amount { get; init; }

    /// <summary>
    /// This container returns jurisdiction information about region-specific fees that are charged to sellers.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("feeJurisdiction")]
    public FeeJurisdiction? FeeJurisdiction { get; init; }

    /// <summary>
    /// A description of the fee that was deducted from the seller payout.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("feeMemo")]
    public string? FeeMemo { get; init; }

    /// <summary>
    /// The enumeration value returned here indicates the type of fee that was deducted from the seller payout. For implementation help, refer to &lt;a href='https://developer.ebay.com/api-docs/sell/finances/types/api:FeeTypeEnum'&gt;eBay API documentation&lt;/a&gt;
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("feeType")]
    public string? FeeType { get; init; }
}
