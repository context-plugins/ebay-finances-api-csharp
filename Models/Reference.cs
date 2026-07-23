using System.Text.Json.Serialization;

namespace EBayFinancesApi.Models;

/// <summary>
/// This field is returned for NON_SALE_CHARGE transactions that contain non-transactional seller fees.
/// </summary>
public record Reference
{
    /// <summary>
    /// The identifier of the transaction as specified by the &lt;strong&gt;referenceType&lt;/strong&gt;. For example, with a &lt;strong&gt;referenceType&lt;/strong&gt; of &lt;strong&gt;item_id&lt;/strong&gt;, the &lt;strong&gt;referenceId&lt;/strong&gt; refers to a unique item. This item may have several &lt;code&gt;NON_SALE_CHARGE&lt;/code&gt; transactions.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("referenceId")]
    public string? ReferenceId { get; init; }

    /// <summary>
    /// An enumeration value that identifies the reference type associated with the &lt;strong&gt;referenceId&lt;/strong&gt;. For implementation help, refer to &lt;a href='https://developer.ebay.com/api-docs/sell/finances/types/pay:ReferenceTypeEnum'&gt;eBay API documentation&lt;/a&gt;
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("referenceType")]
    public string? ReferenceType { get; init; }
}
