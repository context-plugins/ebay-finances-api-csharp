using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EBayFinancesApi.Models;

/// <summary>
/// This type is used to express the details of one of the following monetary transactions: a buyer's payment for an order, a refund to the buyer for a returned item or cancelled order, or a credit issued by eBay to the seller's account.
/// </summary>
public record Transaction
{
    /// <summary>
    /// This type is used to express the dollar value and currency used for any transaction retrieved with the &lt;strong&gt;Finances API&lt;/strong&gt;, including an order total, a seller payout, a buyer refund, or a seller credit.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("amount")]
    public Amount? Amount { get; init; }

    /// <summary>
    /// The enumeration value returned in this field indicates if the monetary transaction amount is a (&lt;code&gt;CREDIT&lt;/code&gt;) or a (&lt;code&gt;DEBIT&lt;/code&gt;) to the seller's account. Typically, the &lt;code&gt;SALE&lt;/code&gt; and &lt;code&gt;CREDIT&lt;/code&gt; transaction types are credits to the seller's account, and the &lt;code&gt;REFUND&lt;/code&gt;, &lt;code&gt;DISPUTE&lt;/code&gt;, &lt;code&gt;SHIPPING_LABEL&lt;/code&gt;, and &lt;code&gt;TRANSFER&lt;/code&gt; transaction types are debits to the seller's account. For implementation help, refer to &lt;a href='https://developer.ebay.com/api-docs/sell/finances/types/pay:BookingEntryEnum'&gt;eBay API documentation&lt;/a&gt;
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("bookingEntry")]
    public string? BookingEntry { get; init; }

    /// <summary>
    /// This type is used to express details about the buyer associated with an order. At this time, the only field in this type is the eBay user ID of the buyer, but more fields may get added at a later date.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("buyer")]
    public Buyer? Buyer { get; init; }

    /// <summary>
    /// This type is used to express the dollar value and currency used for any transaction retrieved with the &lt;strong&gt;Finances API&lt;/strong&gt;, including an order total, a seller payout, a buyer refund, or a seller credit.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("eBayCollectedTaxAmount")]
    public Amount? EBayCollectedTaxAmount { get; init; }

    /// <summary>
    /// This container returns jurisdiction information about region-specific fees that are charged to sellers.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("feeJurisdiction")]
    public FeeJurisdiction? FeeJurisdiction { get; init; }

    /// <summary>
    /// The type of fee. For implementation help, refer to &lt;a href='https://developer.ebay.com/api-docs/sell/finances/types/api:FeeTypeEnum'&gt;eBay API documentation&lt;/a&gt;
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("feeType")]
    public string? FeeType { get; init; }

    /// <summary>
    /// The unique identifier of the eBay order associated with the monetary transaction.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("orderId")]
    public string? OrderId { get; init; }

    /// <summary>
    /// This array shows the fees that are deducted from a seller payout for each line item in an order.&lt;br&gt;&lt;br&gt;&lt;span class="tablenote"&gt;&lt;strong&gt;Note:&lt;/strong&gt; In some cases, a transaction fee might be returned asynchronously from the associated order. In such cases, you can determine the order to which the fee applies by examining the referenceID value of the fee, which should match the ID of the order.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("orderLineItems")]
    public IReadOnlyList<OrderLineItem>? OrderLineItems { get; init; }

    /// <summary>
    /// This string value indicates the entity that is processing  the payment.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("paymentsEntity")]
    public string? PaymentsEntity { get; init; }

    /// <summary>
    /// The unique identifier of the seller payout associated with the monetary transaction. This identifier is generated once eBay begins processing the payout for the corresponding order. This field will not be returned if eBay has not yet begun processing the payout for an order.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("payoutId")]
    public string? PayoutId { get; init; }

    /// <summary>
    /// This field contains reference information for the transaction fee. This includes an ID and the type of ID provided (such as item ID).
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("references")]
    public IReadOnlyList<Reference>? References { get; init; }

    /// <summary>
    /// The Sales Record Number associated with a sales order. Sales Record Numbers are Selling Manager/Selling Manager Pro identifiers that are created at order checkout.&lt;br&gt;&lt;br&gt;&lt;span class="tablenote"&gt;&lt;strong&gt;Note:&lt;/strong&gt; For all orders originating after February 1, 2020, a value of &lt;code&gt;0&lt;/code&gt; will be returned in this field. The Sales Record Number field has also been removed from Seller Hub. Instead of &lt;strong&gt;salesRecordReference&lt;/strong&gt;, depend on &lt;strong&gt;orderId&lt;/strong&gt; instead as the identifier of the order. The &lt;strong&gt;salesRecordReference&lt;/strong&gt; field has been scheduled for deprecation, and a date for when this field will no longer be returned at all will be announced soon.&lt;/span&gt;
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("salesRecordReference")]
    public string? SalesRecordReference { get; init; }

    /// <summary>
    /// This type is used to express the dollar value and currency used for any transaction retrieved with the &lt;strong&gt;Finances API&lt;/strong&gt;, including an order total, a seller payout, a buyer refund, or a seller credit.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("totalFeeAmount")]
    public Amount? TotalFeeAmount { get; init; }

    /// <summary>
    /// This type is used to express the dollar value and currency used for any transaction retrieved with the &lt;strong&gt;Finances API&lt;/strong&gt;, including an order total, a seller payout, a buyer refund, or a seller credit.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("totalFeeBasisAmount")]
    public Amount? TotalFeeBasisAmount { get; init; }

    /// <summary>
    /// This timestamp indicates when the monetary transaction (order purchase, buyer refund, seller credit) occurred. The following (UTC) format is used: &lt;code&gt;YYYY-MM-DDTHH:MM:SS.SSSZ&lt;/code&gt;. For example, &lt;code&gt;2015-08-04T19:09:02.768Z&lt;/code&gt;.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("transactionDate")]
    public string? TransactionDate { get; init; }

    /// <summary>
    /// The unique identifier of the monetary transaction. A monetary transaction can be a sales order, an order refund to the buyer, a credit to the seller's account, a debit to the seller for the purchase of a shipping label, or a transaction where eBay recouped money from the seller if the seller lost a buyer-initiated payment dispute.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("transactionId")]
    public string? TransactionId { get; init; }

    /// <summary>
    /// This field provides more details on shipping label transactions and transactions where the funds are being held by eBay. For shipping label transactions, the &lt;b&gt;transactionMemo&lt;/b&gt; gives details about a purchase, a refund, or a price adjustment to the cost of the shipping label. For on-hold transactions, the &lt;b&gt;transactionMemo&lt;/b&gt; provides information on the reason for the hold or when the hold will be released (e.g., "Funds on hold. Estimated release on Jun 1").&lt;br&gt;&lt;br&gt;This field is only returned if applicable/available.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("transactionMemo")]
    public string? TransactionMemo { get; init; }

    /// <summary>
    /// This enumeration value indicates the current status of the seller payout associated with the monetary transaction. See the &lt;code&gt;TransactionStatusEnum&lt;/code&gt; type for more information on the different states. For implementation help, refer to &lt;a href='https://developer.ebay.com/api-docs/sell/finances/types/pay:TransactionStatusEnum'&gt;eBay API documentation&lt;/a&gt;
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("transactionStatus")]
    public string? TransactionStatus { get; init; }

    /// <summary>
    /// This enumeration value indicates the type of monetary transaction. Examples of monetary transactions include a buyer's payment for an order, a refund to the buyer for a returned item or cancelled order, or a credit issued by eBay to the seller's account. For a complete list of monetary transaction types within the &lt;strong&gt;Finances API&lt;/strong&gt;, see the &lt;a href="/api-docs/sell/finances/types/pay:TransactionTypeEnum"&gt;TransactionTypeEnum&lt;/a&gt; type. For implementation help, refer to &lt;a href='https://developer.ebay.com/api-docs/sell/finances/types/pay:TransactionTypeEnum'&gt;eBay API documentation&lt;/a&gt;
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("transactionType")]
    public string? TransactionType { get; init; }
}
