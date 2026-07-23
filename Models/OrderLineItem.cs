using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EBayFinancesApi.Models;

/// <summary>
/// This type is used to show the fees that are deducted from a seller payout for each line item in an order.
/// </summary>
public record OrderLineItem
{
    /// <summary>
    /// This type is used to express the dollar value and currency used for any transaction retrieved with the &lt;strong&gt;Finances API&lt;/strong&gt;, including an order total, a seller payout, a buyer refund, or a seller credit.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("feeBasisAmount")]
    public Amount? FeeBasisAmount { get; init; }

    /// <summary>
    /// The unique identifier of an order line item.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("lineItemId")]
    public string? LineItemId { get; init; }

    /// <summary>
    /// An array of all fees accrued for the order line item and deducted from a seller payout.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("marketplaceFees")]
    public IReadOnlyList<Fee>? MarketplaceFees { get; init; }
}
