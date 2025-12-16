using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    [SerializationModel]
    internal record CoinbaseConvertQuoteWrapper
    {
        [JsonPropertyName("trade")]
        public CoinbaseConvertQuote Trade { get; set; } = null!;
    }

    /// <summary>
    /// Convert quote
    /// </summary>
    [SerializationModel]
    public record CoinbaseConvertQuote
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// User entered quantity
        /// </summary>
        [JsonPropertyName("user_entered_amount")]
        public CoinbaseQuantityReference UserEnteredQuantity { get; set; } = null!;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public CoinbaseQuantityReference Quantity { get; set; } = null!;
        /// <summary>
        /// Subtotal
        /// </summary>
        [JsonPropertyName("subtotal")]
        public CoinbaseQuantityReference Subtotal { get; set; } = null!;
        /// <summary>
        /// Total
        /// </summary>
        [JsonPropertyName("total")]
        public CoinbaseQuantityReference Total { get; set; } = null!;
        /// <summary>
        /// Fees
        /// </summary>
        [JsonPropertyName("fees")]
        public CoinbaseConvertFees[] Fees { get; set; } = null!;
        /// <summary>
        /// Total fee
        /// </summary>
        [JsonPropertyName("total_fee")]
        public CoinbaseConvertFees TotalFee { get; set; } = null!;
        /// <summary>
        /// Unit price
        /// </summary>
        [JsonPropertyName("unit_price")]
        public CoinbaseUnitPrice UnitPrice { get; set; } = null!;
        /// <summary>
        /// Unit warnings
        /// </summary>
        [JsonPropertyName("user_warnings")]
        public CoinbaseUserWarning[] UserWarnings { get; set; } = null!;

        /// <summary>
        /// User reference
        /// </summary>
        [JsonPropertyName("user_reference")]
        public string? UserReference { get; set; }
        /// <summary>
        /// Source asset
        /// </summary>
        [JsonPropertyName("source_currency")]
        public string SourceAsset { get; set; } = string.Empty;
        /// <summary>
        /// Target asset
        /// </summary>
        [JsonPropertyName("target_currency")]
        public string TargetAsset { get; set; } = string.Empty;
        /// <summary>
        /// Source id
        /// </summary>
        [JsonPropertyName("source_id")]
        public string SourceId { get; set; } = string.Empty;
        /// <summary>
        /// Target id
        /// </summary>
        [JsonPropertyName("target_id")]
        public string TargetId { get; set; } = string.Empty;
        /// <summary>
        /// Cancellation reason
        /// </summary>
        [JsonPropertyName("cancellation_reason")]
        public CoinbaseCancellationReason? CancellationReason { get; set; }
        /// <summary>
        /// Subscription info
        /// </summary>
        [JsonPropertyName("subscription_info")]
        public CoinbaseSubscriptionInfo? SubscriptionInfo { get; set; }
        /// <summary>
        /// Exchange rate
        /// </summary>
        [JsonPropertyName("exchange_rate")]
        public CoinbaseQuantityReference ExchangeRate { get; set; } = null!;
        /// <summary>
        /// Tax details
        /// </summary>
        [JsonPropertyName("tax_details")]
        public CoinbaseTaxDetails[] TaxDetails { get; set; } = Array.Empty<CoinbaseTaxDetails>();

        /// <summary>
        /// Trade incentive info
        /// </summary>
        [JsonPropertyName("trade_incentive_info")]
        public CoinbaseTradeIncentiveInfo TradeIncentiveInfo { get; set; } = null!;
        /// <summary>
        /// Total fee without tax
        /// </summary>
        [JsonPropertyName("total_fee_without_tax")]
        public CoinbaseConvertFees TotalFeeWithoutTax { get; set; } = null!;
        /// <summary>
        /// Fiat denoted total
        /// </summary>
        [JsonPropertyName("fiat_denoted_total")]
        public CoinbaseQuantityReference FiatDenotedTotal { get; set; } = null!;
    }

    /// <summary>
    /// Convert fee info
    /// </summary>
    [SerializationModel]
    public record CoinbaseConvertFees
    {
        /// <summary>
        /// Title
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Description
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Label
        /// </summary>
        [JsonPropertyName("label")]
        public string Label { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public CoinbaseQuantityReference Quantity { get; set; } = null!;
        /// <summary>
        /// Disclosure
        /// </summary>
        [JsonPropertyName("disclosure")]
        public CoinbaseConvertFeeDisclosure? Disclosure { get; set; }
    }

    /// <summary>
    /// Convert fee disclosure
    /// </summary>
    [SerializationModel]
    public record CoinbaseConvertFeeDisclosure
    {
        /// <summary>
        /// Title
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Description
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Link
        /// </summary>
        [JsonPropertyName("link")]
        public CoinbaseLink? Link { get; set; }
    }

    /// <summary>
    /// Convert fee disclosure
    /// </summary>
    [SerializationModel]
    public record CoinbaseLink
    {
        /// <summary>
        /// Text
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;
        /// <summary>
        /// Url
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }

    /// <summary>
    /// Convert unit price
    /// </summary>
    [SerializationModel]
    public record CoinbaseUnitPrice
    {
        /// <summary>
        /// Target to fiat
        /// </summary>
        [JsonPropertyName("target_to_fiat")]
        public CoinbaseQuantityScale TargetToFiat { get; set; } = null!;
        /// <summary>
        /// Target to source
        /// </summary>
        [JsonPropertyName("target_to_source")]
        public CoinbaseQuantityScale TargetToSource { get; set; } = null!;
        /// <summary>
        /// Source to fiat
        /// </summary>
        [JsonPropertyName("source_to_fiat")]
        public CoinbaseQuantityScale SourceToFiat { get; set; } = null!;
    }

    /// <summary>
    /// Convert fee disclosure
    /// </summary>
    [SerializationModel]
    public record CoinbaseQuantityScale
    {
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public CoinbaseQuantityReference Quantity { get; set; } = null!;
        /// <summary>
        /// Scale
        /// </summary>
        [JsonPropertyName("scale")]
        public int Scale { get; set; }
    }

    /// <summary>
    /// User warning
    /// </summary>
    [SerializationModel]
    public record CoinbaseUserWarning
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Code
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// Message
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// Link
        /// </summary>
        [JsonPropertyName("link")]
        public CoinbaseLink? Link { get; set; }
        /// <summary>
        /// Context
        /// </summary>
        [JsonPropertyName("context")]
        public CoinbaseContext? Context { get; set; }
    }

    /// <summary>
    /// User warning
    /// </summary>
    [SerializationModel]
    public record CoinbaseContext
    {
        /// <summary>
        /// Details
        /// </summary>
        [JsonPropertyName("details")]
        public string[] Details { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Title
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Link text
        /// </summary>
        [JsonPropertyName("link_text")]
        public string LinkText { get; set; } = string.Empty;
    }

    /// <summary>
    /// Cancellation reason
    /// </summary>
    [SerializationModel]
    public record CoinbaseCancellationReason
    {
        /// <summary>
        /// Code
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// Message
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// Error code
        /// </summary>
        [JsonPropertyName("error_code")]
        public string ErrorCode { get; set; } = string.Empty;
        /// <summary>
        /// Error cta
        /// </summary>
        [JsonPropertyName("error_cta")]
        public string ErrorCta { get; set; } = string.Empty;
    }

    /// <summary>
    /// Tax details
    /// </summary>
    [SerializationModel]
    public record CoinbaseTaxDetails
    {
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public CoinbaseQuantityReference Quantity { get; set; } = null!;
    }

    /// <summary>
    /// Subscription info
    /// </summary>
    [SerializationModel]
    public record CoinbaseSubscriptionInfo
    {
        /// <summary>
        /// Free trading reset date
        /// </summary>
        [JsonPropertyName("free_trading_reset_date")]
        public DateTime? FreeTradingResetDate { get; set; }
        /// <summary>
        /// Used zero-fee trading
        /// </summary>
        [JsonPropertyName("used_zero_fee_trading")]
        public CoinbaseQuantityReference UsedZeroFeeTrading { get; set; } = null!;
        /// <summary>
        /// Remaining free trading volume
        /// </summary>
        [JsonPropertyName("remaining_free_trading_volume")]
        public CoinbaseQuantityReference RemainingFreeTradingVolume { get; set; } = null!;
        /// <summary>
        /// Max free trading volume
        /// </summary>
        [JsonPropertyName("max_free_trading_volume")]
        public CoinbaseQuantityReference MaxFreeTradingVolume { get; set; } = null!;
        /// <summary>
        /// Has benefit cap
        /// </summary>
        [JsonPropertyName("has_benefit_cap")]
        public bool? HasBenifitCap { get; set; }
        /// <summary>
        /// Applied subscription benefit
        /// </summary>
        [JsonPropertyName("applied_subscription_benefit")]
        public bool? AppliedSubscriptionBenefit { get; set; }
        /// <summary>
        /// Fee without subscription benefit
        /// </summary>
        [JsonPropertyName("fee_without_subscription_benefit")]
        public CoinbaseQuantityReference FeeWithoutSubscriptionBenefit { get; set; } = null!;
        /// <summary>
        /// Payment method fee without subscription benefit
        /// </summary>
        [JsonPropertyName("payment_method_fee_without_subscription_benefit")]
        public CoinbaseQuantityReference PaymentMethodFeeWithoutSubscriptionBenefit { get; set; } = null!;
    }

    /// <summary>
    /// Trade incentive info
    /// </summary>
    [SerializationModel]
    public record CoinbaseTradeIncentiveInfo
    {
        /// <summary>
        /// Applied incentive
        /// </summary>
        [JsonPropertyName("applied_incentive")]
        public bool AppliedIncentive { get; set; }
        /// <summary>
        /// User incentive id
        /// </summary>
        [JsonPropertyName("user_incentive_id")]
        public string UserIncentiveId { get; set; } = string.Empty;
        /// <summary>
        /// Promo code value
        /// </summary>
        [JsonPropertyName("code_val")]
        public string PromoCode { get; set; } = string.Empty;
        /// <summary>
        /// End time
        /// </summary>
        [JsonPropertyName("ends_at")]
        public DateTime? EndsAt { get; set; }
        /// <summary>
        /// Fee without incentive
        /// </summary>
        [JsonPropertyName("fee_without_incentive")]
        public CoinbaseQuantityReference Quantity { get; set; } = null!;
        /// <summary>
        /// Redeemed
        /// </summary>
        [JsonPropertyName("redeemed")]
        public bool? Redeemed { get; set; }
    }
}
