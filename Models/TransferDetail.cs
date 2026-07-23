using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EBayFinancesApi.Models;

/// <summary>
/// This type is used by the &lt;b&gt;transferDetail&lt;/b&gt; container, which provides more details about the transfer and the charge(s) associated with the transfer.
/// </summary>
public record TransferDetail
{
    /// <summary>
    /// This type is used by the &lt;b&gt;balanceAdjustment&lt;/b&gt; container, which shows the seller payout balance that will be applied toward the charges outlined in the &lt;b&gt;charges&lt;/b&gt; array.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("balanceAdjustment")]
    public BalanceAdjustment? BalanceAdjustment { get; init; }

    /// <summary>
    /// This container is an array of one or more charges related to the transfer. Charges can be related to an order cancellation, order return, case, payment dispute, etc.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("charges")]
    public IReadOnlyList<Charge>? Charges { get; init; }

    /// <summary>
    /// This type is used to express the dollar value and currency used for any transaction retrieved with the &lt;strong&gt;Finances API&lt;/strong&gt;, including an order total, a seller payout, a buyer refund, or a seller credit.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("totalChargeNetAmount")]
    public Amount? TotalChargeNetAmount { get; init; }
}
