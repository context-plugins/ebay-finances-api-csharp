using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EBayFinancesApi.Models;

/// <summary>
/// This type defines the fields that can be returned in an error.
/// </summary>
public record Error
{
    /// <summary>
    /// Identifies the type of erro.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("category")]
    public string? Category { get; init; }

    /// <summary>
    /// Name for the primary system where the error occurred. This is relevant for application errors.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("domain")]
    public string? Domain { get; init; }

    /// <summary>
    /// A unique number to identify the error.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("errorId")]
    public int? ErrorId { get; init; }

    /// <summary>
    /// An array of request elements most closely associated to the error.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("inputRefIds")]
    public IReadOnlyList<string>? InputRefIds { get; init; }

    /// <summary>
    /// A more detailed explanation of the error.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("longMessage")]
    public string? LongMessage { get; init; }

    /// <summary>
    /// Information on how to correct the problem, in the end user's terms and language where applicable.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("message")]
    public string? Message { get; init; }

    /// <summary>
    /// An array of request elements most closely associated to the error.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("outputRefIds")]
    public IReadOnlyList<string>? OutputRefIds { get; init; }

    /// <summary>
    /// An array of name/value pairs that describe details the error condition. These are useful when multiple errors are returned.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("parameters")]
    public IReadOnlyList<ErrorParameter>? Parameters { get; init; }

    /// <summary>
    /// Further helps indicate which subsystem the error is coming from. System subcategories include: Initialization, Serialization, Security, Monitoring, Rate Limiting, etc.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("subdomain")]
    public string? Subdomain { get; init; }
}
