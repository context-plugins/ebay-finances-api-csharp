using System.Text.Json.Serialization;

namespace EBayFinancesApi.Models;

/// <summary>
/// This type is used to express details about the buyer associated with an order. At this time, the only field in this type is the eBay user ID of the buyer, but more fields may get added at a later date.
/// </summary>
public record Buyer
{
    /// <summary>
    /// The eBay user ID of the order's buyer.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("username")]
    public string? Username { get; init; }
}
