using System.Text.Json.Serialization;

namespace EBayFinancesApi.Models;

/// <summary>
/// This type provided details on the funding source for the transfer.
/// </summary>
public record FundingSource
{
    /// <summary>
    /// The brand name of the credit card or the name of the financial institution that is the source of payment. This field may not be populated for other funding sources.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("brand")]
    public string? Brand { get; init; }

    /// <summary>
    /// This field provides a note about the funding source. If the seller's credit card or bank account is the funding source, this field might contain the last four digits of the credit card or bank account. This field may also be returned as null.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("memo")]
    public string? Memo { get; init; }

    /// <summary>
    /// The string value returned here indicates the funding source. Possible values include the following:&lt;ul&gt;&lt;li&gt;&lt;code&gt;AVAILABLE_FUNDS&lt;/code&gt;: transfer is funded with seller payout funds&lt;/li&gt;&lt;li&gt;&lt;code&gt;CREDIT_CARD&lt;/code&gt;: transfer is funded with seller's credit card&lt;/li&gt;&lt;li&gt;&lt;code&gt;BANK&lt;/code&gt;: transfer is funded with a direct debit to seller's bank account on file with eBay&lt;/li&gt;&lt;li&gt;&lt;code&gt;PAY_UPON_INVOICE&lt;/code&gt;: eBay will bill the seller for the transfer on the monthly invoice&lt;/li&gt;&lt;/ul&gt;
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("type")]
    public string? Type { get; init; }
}
