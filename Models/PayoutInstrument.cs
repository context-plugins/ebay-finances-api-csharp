using System.Text.Json.Serialization;

namespace EBayFinancesApi.Models;

/// <summary>
/// This type provides details about the seller's account that received (or is scheduled to receive) a payout.
/// </summary>
public record PayoutInstrument
{
    /// <summary>
    /// This value is the last four digits of the account that the seller uses to receive the payout. This may be the last four digits of a bank account, a debit card, or a payment processor account such as Payoneer.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("accountLastFourDigits")]
    public string? AccountLastFourDigits { get; init; }

    /// <summary>
    /// This value indicates the type of account that received the payout. The value returned in this field may be:&lt;br&gt;&lt;ul&gt;&lt;li&gt;&lt;code&gt;BANK&lt;/code&gt;: indicates that the payout was made to a seller's bank account.&lt;/li&gt;&lt;li&gt;&lt;code&gt;CARD&lt;/code&gt;: indicates that the payout went to a seller's debit card&lt;/li&gt;&lt;li&gt;The name of a digital wallet provider or payment processor (e.g., &lt;code&gt;PAYONEER&lt;/code&gt;)&lt;/li&gt;&lt;/ul&gt;
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("instrumentType")]
    public string? InstrumentType { get; init; }

    /// <summary>
    /// When &lt;b&gt;instrumentType&lt;/b&gt; returns &lt;code&gt;BANK&lt;/code&gt;, this value is the seller-provided nickname that the seller uses to represent the bank account that receives the payout.&lt;br&gt;&lt;br&gt;When &lt;b&gt;instrumentType&lt;/b&gt; returns &lt;code&gt;CARD&lt;/code&gt;, this value is the debit card network for the debit card that receives the payout.&lt;br&gt;&lt;br&gt;When &lt;b&gt;instrumentType&lt;/b&gt; returns a provider of digital wallet or payment processing services, the value returned is the name of the service provider (e.g., &lt;code&gt;PAYONEER&lt;/code&gt;).
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("nickname")]
    public string? Nickname { get; init; }
}
