using System.Text.Json.Serialization;

namespace EBayFinancesApi.Models;

/// <summary>
/// This type is used by the &lt;b&gt;charge&lt;/b&gt; container, which is an array of one or more charges related to the transfer.
/// </summary>
public record Charge
{
    /// <summary>
    /// The unique identifier of an order cancellation. This field is only applicable and returned if the charge is related to an order  cancellation.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("cancellationId")]
    public string? CancellationId { get; init; }

    /// <summary>
    /// The unique identifier of a case filed against an order. This field is only applicable and returned if the charge is related to a case filed against an order.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("caseId")]
    public string? CaseId { get; init; }

    /// <summary>
    /// This type is used to express the dollar value and currency used for any transaction retrieved with the &lt;strong&gt;Finances API&lt;/strong&gt;, including an order total, a seller payout, a buyer refund, or a seller credit.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("chargeNetAmount")]
    public Amount? ChargeNetAmount { get; init; }

    /// <summary>
    /// The unique identifier of an Item Not Received (INR) inquiry filed against an order. This field is only applicable and returned if the charge is related to has an INR inquiry filed against the order.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("inquiryId")]
    public string? InquiryId { get; init; }

    /// <summary>
    /// The unique identifier of the order that is associated with the charge.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("orderId")]
    public string? OrderId { get; init; }

    /// <summary>
    /// The unique identifier of a third-party payment dispute filed against an order. This occurs when the buyer files a dispute against the order with their payment provider, and then the dispute comes into eBay's system. This field is only applicable and returned if the charge is related to a third-party payment dispute filed against an order.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("paymentDisputeId")]
    public string? PaymentDisputeId { get; init; }

    /// <summary>
    /// The unique identifier of a buyer refund associated with the charge.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("refundId")]
    public string? RefundId { get; init; }

    /// <summary>
    /// The unique identifier of an order return. This field is only applicable and returned if the charge is related to an order that was returned by the buyer.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("returnId")]
    public string? ReturnId { get; init; }
}
