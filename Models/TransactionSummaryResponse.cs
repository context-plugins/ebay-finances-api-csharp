using System.Text.Json.Serialization;

namespace EBayFinancesApi.Models;

/// <summary>
/// This type is the base response type of the &lt;strong&gt;getTransactionSummary&lt;/strong&gt; method, and based on the filters that are used in the &lt;strong&gt;getTransactionSummary&lt;/strong&gt; call URI, the response may include  total count and amount of the seller's sales and credits, total count and amount of buyer refunds, and total count and amount of seller payment holds.
/// </summary>
public record TransactionSummaryResponse
{
    /// <summary>
    /// This type is used to express the dollar value and currency used for any transaction retrieved with the &lt;strong&gt;Finances API&lt;/strong&gt;, including an order total, a seller payout, a buyer refund, or a seller credit.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("adjustmentAmount")]
    public Amount? AdjustmentAmount { get; init; }

    /// <summary>
    /// The credit debit sign indicator for adjustment. For implementation help, refer to &lt;a href='https://developer.ebay.com/api-docs/sell/finances/types/pay:BookingEntryEnum'&gt;eBay API documentation&lt;/a&gt;
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("adjustmentBookingEntry")]
    public string? AdjustmentBookingEntry { get; init; }

    /// <summary>
    /// Total adjustment count for given payee within a specified period.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("adjustmentCount")]
    public int? AdjustmentCount { get; init; }

    /// <summary>
    /// This type is used to express the dollar value and currency used for any transaction retrieved with the &lt;strong&gt;Finances API&lt;/strong&gt;, including an order total, a seller payout, a buyer refund, or a seller credit.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("balanceTransferAmount")]
    public Amount? BalanceTransferAmount { get; init; }

    /// <summary>
    /// The credit debit sign indicator for the balance transfer. For implementation help, refer to &lt;a href='https://developer.ebay.com/api-docs/sell/finances/types/pay:BookingEntryEnum'&gt;eBay API documentation&lt;/a&gt;
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("balanceTransferBookingEntry")]
    public string? BalanceTransferBookingEntry { get; init; }

    /// <summary>
    /// The total balance transfer count for given payee within the specified period.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("balanceTransferCount")]
    public int? BalanceTransferCount { get; init; }

    /// <summary>
    /// This type is used to express the dollar value and currency used for any transaction retrieved with the &lt;strong&gt;Finances API&lt;/strong&gt;, including an order total, a seller payout, a buyer refund, or a seller credit.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("creditAmount")]
    public Amount? CreditAmount { get; init; }

    /// <summary>
    /// The enumeration value indicates whether the dollar amount in the &lt;strong&gt;creditAmount&lt;/strong&gt; field is a charge (debit) to the seller or a credit. Typically, the enumeration value returned here will be &lt;code&gt;CREDIT&lt;/code&gt;. For implementation help, refer to &lt;a href='https://developer.ebay.com/api-docs/sell/finances/types/pay:BookingEntryEnum'&gt;eBay API documentation&lt;/a&gt;
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("creditBookingEntry")]
    public string? CreditBookingEntry { get; init; }

    /// <summary>
    /// This integer value indicates the total number of the seller's sales and/or credits that match the input criteria. &lt;br&gt;&lt;br&gt;&lt;span class="tablenote"&gt;&lt;strong&gt;Note:&lt;/strong&gt; Unless the &lt;b&gt;transactionType&lt;/b&gt; filter is used in the request to retrieve a specific type of monetary transaction (sale, buyer refund, or seller credit), the &lt;b&gt;creditCount&lt;/b&gt; and &lt;b&gt;creditAmount&lt;/b&gt; fields account for both order sales and seller credits (the count and value is not distinguished between the two monetary transaction types).&lt;/span&gt;&lt;br&gt;&lt;br&gt;This field is generally returned, even if &lt;code&gt;0&lt;/code&gt;, but it will not be returned if a &lt;strong&gt;transactionType&lt;/strong&gt; filter is used, and its value is set to either &lt;code&gt;REFUND&lt;/code&gt;, &lt;code&gt;DISPUTE&lt;/code&gt;, or &lt;code&gt;SHIPPING_LABEL&lt;/code&gt;.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("creditCount")]
    public int? CreditCount { get; init; }

    /// <summary>
    /// This type is used to express the dollar value and currency used for any transaction retrieved with the &lt;strong&gt;Finances API&lt;/strong&gt;, including an order total, a seller payout, a buyer refund, or a seller credit.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("disputeAmount")]
    public Amount? DisputeAmount { get; init; }

    /// <summary>
    /// The enumeration value indicates whether the dollar amount in the &lt;strong&gt;disputeAmount&lt;/strong&gt; field is a charge (debit) to the seller or a credit. Typically, the enumeration value returned here will be &lt;code&gt;DEBIT&lt;/code&gt;, but its possible that &lt;code&gt;CREDIT&lt;/code&gt; could be returned if the seller contested one or more payment disputes and won the dispute. For implementation help, refer to &lt;a href='https://developer.ebay.com/api-docs/sell/finances/types/pay:BookingEntryEnum'&gt;eBay API documentation&lt;/a&gt;
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("disputeBookingEntry")]
    public string? DisputeBookingEntry { get; init; }

    /// <summary>
    /// This integer value indicates the total number of payment disputes that have been initiated by one or more buyers. Only the orders that match the input criteria are considered. The Payment Disputes methods in the Fulfillment API can be used by the seller to retrieve more information about any payment disputes. &lt;br&gt;&lt;br&gt;This field is generally returned, even if &lt;code&gt;0&lt;/code&gt;, but it will not be returned if a &lt;strong&gt;transactionType&lt;/strong&gt; filter is used, and its value is set to any value other than &lt;code&gt;DISPUTE&lt;/code&gt;.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("disputeCount")]
    public int? DisputeCount { get; init; }

    /// <summary>
    /// This type is used to express the dollar value and currency used for any transaction retrieved with the &lt;strong&gt;Finances API&lt;/strong&gt;, including an order total, a seller payout, a buyer refund, or a seller credit.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("loanRepaymentAmount")]
    public Amount? LoanRepaymentAmount { get; init; }

    /// <summary>
    /// The enumeration value indicates whether the &lt;code&gt;loanRepaymentAmount&lt;/code&gt; is a &lt;code&gt;DEBIT&lt;/code&gt; against, or a &lt;code&gt;CREDIT&lt;/code&gt; to, the sellers's account.&lt;br&gt;&lt;br&gt;For most &lt;code&gt;loanRepaymentAmount&lt;/code&gt; transactions, &lt;code&gt;loanRepaymentBookingEntry&lt;/code&gt; will be &lt;b&gt;DEBIT&lt;/b&gt;. However, if a loan repayment transaction is reversed, that transaction will be shown as a &lt;b&gt;CREDIT&lt;/b&gt;. For implementation help, refer to &lt;a href='https://developer.ebay.com/api-docs/sell/finances/types/pay:BookingEntryEnum'&gt;eBay API documentation&lt;/a&gt;
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("loanRepaymentBookingEntry")]
    public string? LoanRepaymentBookingEntry { get; init; }

    /// <summary>
    /// This integer value indicates the total number of &lt;code&gt;LOAN_REPAYMENT&lt;/code&gt; transactions (i.e., &lt;code&gt;DEBIT&lt;/code&gt; and &lt;code&gt;CREDIT&lt;/code&gt;,) that match the input criteria.&lt;br&gt;&lt;br&gt;This field is generally returned even if it equals &lt;b&gt;0&lt;/b&gt;. However it will not be returned if a &lt;code&gt;transactionType&lt;/code&gt; filter is used and its value has been set to any enumeration value other than &lt;code&gt;LOAN_REPAYMENT&lt;/code&gt;.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("loanRepaymentCount")]
    public int? LoanRepaymentCount { get; init; }

    /// <summary>
    /// This type is used to express the dollar value and currency used for any transaction retrieved with the &lt;strong&gt;Finances API&lt;/strong&gt;, including an order total, a seller payout, a buyer refund, or a seller credit.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("nonSaleChargeAmount")]
    public Amount? NonSaleChargeAmount { get; init; }

    /// <summary>
    /// The credit/debit sign indicator for the non-sale charge. For implementation help, refer to &lt;a href='https://developer.ebay.com/api-docs/sell/finances/types/pay:BookingEntryEnum'&gt;eBay API documentation&lt;/a&gt;
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("nonSaleChargeBookingEntry")]
    public string? NonSaleChargeBookingEntry { get; init; }

    /// <summary>
    /// The total non-sale charge count for given payee within a specified period.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("nonSaleChargeCount")]
    public int? NonSaleChargeCount { get; init; }

    /// <summary>
    /// This type is used to express the dollar value and currency used for any transaction retrieved with the &lt;strong&gt;Finances API&lt;/strong&gt;, including an order total, a seller payout, a buyer refund, or a seller credit.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("onHoldAmount")]
    public Amount? OnHoldAmount { get; init; }

    /// <summary>
    /// The enumeration value indicates whether the dollar amount in the &lt;strong&gt;onHoldAmount&lt;/strong&gt; field is a charge (debit) to the seller or a credit. Typically, the enumeration value returned here will be &lt;code&gt;CREDIT&lt;/code&gt;, since on-hold funds should eventually be released as part of a payout to the seller once the hold is cleared. For implementation help, refer to &lt;a href='https://developer.ebay.com/api-docs/sell/finances/types/pay:BookingEntryEnum'&gt;eBay API documentation&lt;/a&gt;
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("onHoldBookingEntry")]
    public string? OnHoldBookingEntry { get; init; }

    /// <summary>
    /// This integer value indicates the total number of order sales where the associated funds are on hold. Only the orders that match the input criteria are considered.&lt;br&gt;&lt;br&gt;This field is generally returned, even if &lt;code&gt;0&lt;/code&gt;, but it will not be returned if a &lt;strong&gt;transactionStatus&lt;/strong&gt; filter is used, and its value is set to any value other than &lt;code&gt;FUNDS_ON_HOLD&lt;/code&gt;.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("onHoldCount")]
    public int? OnHoldCount { get; init; }

    /// <summary>
    /// This type is used to express the dollar value and currency used for any transaction retrieved with the &lt;strong&gt;Finances API&lt;/strong&gt;, including an order total, a seller payout, a buyer refund, or a seller credit.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("refundAmount")]
    public Amount? RefundAmount { get; init; }

    /// <summary>
    /// The enumeration value indicates whether the dollar amount in the &lt;strong&gt;refundAmount&lt;/strong&gt; field is a charge (debit) to the seller or a credit. Typically, the enumeration value returned here will be &lt;code&gt;DEBIT&lt;/code&gt; since this a refund from the seller to the buyer. For implementation help, refer to &lt;a href='https://developer.ebay.com/api-docs/sell/finances/types/pay:BookingEntryEnum'&gt;eBay API documentation&lt;/a&gt;
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("refundBookingEntry")]
    public string? RefundBookingEntry { get; init; }

    /// <summary>
    /// This integer value indicates the total number of buyer refunds that match the input criteria. &lt;br&gt;&lt;br&gt;This field is generally returned, even if &lt;code&gt;0&lt;/code&gt;, but it will not be returned if a &lt;strong&gt;transactionType&lt;/strong&gt; filter is used, and its value is set to any value other than &lt;code&gt;REFUND&lt;/code&gt;.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("refundCount")]
    public int? RefundCount { get; init; }

    /// <summary>
    /// This type is used to express the dollar value and currency used for any transaction retrieved with the &lt;strong&gt;Finances API&lt;/strong&gt;, including an order total, a seller payout, a buyer refund, or a seller credit.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("shippingLabelAmount")]
    public Amount? ShippingLabelAmount { get; init; }

    /// <summary>
    /// The enumeration value indicates whether the dollar amount in the &lt;strong&gt;shippingLabelAmount&lt;/strong&gt; field is a charge (debit) to the seller or a credit. Typically, the enumeration value returned here will be &lt;code&gt;DEBIT&lt;/code&gt;, as eBay will charge the seller when eBay shipping labels are purchased, but it can be &lt;code&gt;CREDIT&lt;/code&gt; if the seller was refunded for a shipping label or was possibly overcharged for a shipping label. For implementation help, refer to &lt;a href='https://developer.ebay.com/api-docs/sell/finances/types/pay:BookingEntryEnum'&gt;eBay API documentation&lt;/a&gt;
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("shippingLabelBookingEntry")]
    public string? ShippingLabelBookingEntry { get; init; }

    /// <summary>
    /// This is the total number of eBay shipping labels purchased by the seller. The count returned here may depend on the specified input criteria.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("shippingLabelCount")]
    public int? ShippingLabelCount { get; init; }

    /// <summary>
    /// This type is used to express the dollar value and currency used for any transaction retrieved with the &lt;strong&gt;Finances API&lt;/strong&gt;, including an order total, a seller payout, a buyer refund, or a seller credit.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("transferAmount")]
    public Amount? TransferAmount { get; init; }

    /// <summary>
    /// The enumeration value indicates whether the dollar amount in the &lt;strong&gt;transferAmount&lt;/strong&gt; field is a charge (debit) to the seller or a credit. Typically, the enumeration value returned here will be &lt;code&gt;DEBIT&lt;/code&gt; since this a seller reimbursement to eBay for buyer refunds. For implementation help, refer to &lt;a href='https://developer.ebay.com/api-docs/sell/finances/types/pay:BookingEntryEnum'&gt;eBay API documentation&lt;/a&gt;
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("transferBookingEntry")]
    public string? TransferBookingEntry { get; init; }

    /// <summary>
    /// This integer value indicates the total number of buyer refund transfers that match the input criteria. &lt;br&gt;&lt;br&gt;This field is generally returned, even if &lt;code&gt;0&lt;/code&gt;, but it will not be returned if a &lt;strong&gt;transactionType&lt;/strong&gt; filter is used, and its value is set to any value other than &lt;code&gt;TRANSFER&lt;/code&gt;.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("transferCount")]
    public int? TransferCount { get; init; }

    /// <summary>
    /// This type is used to express the dollar value and currency used for any transaction retrieved with the &lt;strong&gt;Finances API&lt;/strong&gt;, including an order total, a seller payout, a buyer refund, or a seller credit.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("withdrawalAmount")]
    public Amount? WithdrawalAmount { get; init; }

    /// <summary>
    /// The enumeration value indicates whether the dollar amount in the &lt;strong&gt;withdrawalAmount&lt;/strong&gt; field is a charge (debit) to the seller or a credit. Typically, the enumeration value returned here will be &lt;code&gt;DEBIT&lt;/code&gt; since this transaction involves a debit to the seller's available payout funds. For implementation help, refer to &lt;a href='https://developer.ebay.com/api-docs/sell/finances/types/pay:BookingEntryEnum'&gt;eBay API documentation&lt;/a&gt;
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("withdrawalBookingEntry")]
    public string? WithdrawalBookingEntry { get; init; }

    /// <summary>
    /// This integer value indicates the total number of on-demand payouts (withdrawals) that match the input criteria. &lt;br&gt;&lt;br&gt;This field is generally returned, even if &lt;code&gt;0&lt;/code&gt;, but it will not be returned if a &lt;strong&gt;transactionType&lt;/strong&gt; filter is used, and its value is set to any value other than &lt;code&gt;WITHDRAWAL&lt;/code&gt;.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("withdrawalCount")]
    public int? WithdrawalCount { get; init; }
}
