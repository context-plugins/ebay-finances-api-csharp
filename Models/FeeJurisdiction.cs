using System.Text.Json.Serialization;

namespace EBayFinancesApi.Models;

/// <summary>
/// This container returns jurisdiction information about region-specific fees that are charged to sellers.
/// </summary>
public record FeeJurisdiction
{
    /// <summary>
    /// String value that indicates the name of the region to which a region-specific fee applies.&lt;br&gt;&lt;br&gt;The set of valid &lt;b&gt;regionName&lt;/b&gt; values that may be returned is determined by the &lt;b&gt;regionType&lt;/b&gt; value.&lt;br&gt;&lt;br&gt;&lt;span class="tablenote"&gt;&lt;strong&gt;Note:&lt;/strong&gt; Currently, supported &lt;b&gt;regionName&lt;/b&gt; values that may be returned are standard two-character country or state codes.&lt;br&gt;&lt;br&gt;Typical examples include:&lt;ul&gt;&lt;li&gt;&lt;b&gt;MX&lt;/b&gt; [Mexico]&lt;/li&gt;&lt;li&gt;&lt;b&gt;IN&lt;/b&gt; [India]&lt;/li&gt;&lt;li&gt;&lt;b&gt;US&lt;/b&gt; [United States]&lt;/li&gt;&lt;li&gt;CA [California]&lt;/li&gt;&lt;li&gt;VT [Vermont]&lt;/li&gt;&lt;li&gt;ME [Maine]&lt;/li&gt;&lt;/ul&gt;&lt;/span&gt;
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("regionName")]
    public string? RegionName { get; init; }

    /// <summary>
    /// The enumeration value returned here indicates the type of region that is collecting the corresponding fee. For implementation help, refer to &lt;a href='https://developer.ebay.com/api-docs/sell/finances/types/pay:RegionTypeEnum'&gt;eBay API documentation&lt;/a&gt;
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("regionType")]
    public string? RegionType { get; init; }
}
