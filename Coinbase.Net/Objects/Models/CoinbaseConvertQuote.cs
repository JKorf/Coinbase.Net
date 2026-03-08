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
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>user_entered_amount</c>"] User entered quantity
        /// </summary>
        [JsonPropertyName("user_entered_amount")]
        public CoinbaseQuantityReference UserEnteredQuantity { get; set; } = null!;
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public CoinbaseQuantityReference Quantity { get; set; } = null!;
        /// <summary>
        /// ["<c>subtotal</c>"] Subtotal
        /// </summary>
        [JsonPropertyName("subtotal")]
        public CoinbaseQuantityReference Subtotal { get; set; } = null!;
        /// <summary>
        /// ["<c>total</c>"] Total
        /// </summary>
        [JsonPropertyName("total")]
        public CoinbaseQuantityReference Total { get; set; } = null!;
        /// <summary>
        /// ["<c>fees</c>"] Fees
        /// </summary>
        [JsonPropertyName("fees")]
        public CoinbaseConvertFees[] Fees { get; set; } = null!;
        /// <summary>
        /// ["<c>total_fee</c>"] Total fee
        /// </summary>
        [JsonPropertyName("total_fee")]
        public CoinbaseConvertFees TotalFee { get; set; } = null!;
        /// <summary>
        /// ["<c>unit_price</c>"] Unit price
        /// </summary>
        [JsonPropertyName("unit_price")]
        public CoinbaseUnitPrice UnitPrice { get; set; } = null!;
        /// <summary>
        /// ["<c>user_warnings</c>"] Unit warnings
        /// </summary>
        [JsonPropertyName("user_warnings")]
        public CoinbaseUserWarning[] UserWarnings { get; set; } = null!;

        /// <summary>
        /// ["<c>user_reference</c>"] User reference
        /// </summary>
        [JsonPropertyName("user_reference")]
        public string? UserReference { get; set; }
        /// <summary>
        /// ["<c>source_currency</c>"] Source asset
        /// </summary>
        [JsonPropertyName("source_currency")]
        public string SourceAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>target_currency</c>"] Target asset
        /// </summary>
        [JsonPropertyName("target_currency")]
        public string TargetAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>source_id</c>"] Source id
        /// </summary>
        [JsonPropertyName("source_id")]
        public string SourceId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>target_id</c>"] Target id
        /// </summary>
        [JsonPropertyName("target_id")]
        public string TargetId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>cancellation_reason</c>"] Cancellation reason
        /// </summary>
        [JsonPropertyName("cancellation_reason")]
        public CoinbaseCancellationReason? CancellationReason { get; set; }
        /// <summary>
        /// ["<c>subscription_info</c>"] Subscription info
        /// </summary>
        [JsonPropertyName("subscription_info")]
        public CoinbaseSubscriptionInfo? SubscriptionInfo { get; set; }
        /// <summary>
        /// ["<c>exchange_rate</c>"] Exchange rate
        /// </summary>
        [JsonPropertyName("exchange_rate")]
        public CoinbaseQuantityReference ExchangeRate { get; set; } = null!;
        /// <summary>
        /// ["<c>tax_details</c>"] Tax details
        /// </summary>
        [JsonPropertyName("tax_details")]
        public CoinbaseTaxDetails[] TaxDetails { get; set; } = Array.Empty<CoinbaseTaxDetails>();

        /// <summary>
        /// ["<c>trade_incentive_info</c>"] Trade incentive info
        /// </summary>
        [JsonPropertyName("trade_incentive_info")]
        public CoinbaseTradeIncentiveInfo TradeIncentiveInfo { get; set; } = null!;
        /// <summary>
        /// ["<c>total_fee_without_tax</c>"] Total fee without tax
        /// </summary>
        [JsonPropertyName("total_fee_without_tax")]
        public CoinbaseConvertFees TotalFeeWithoutTax { get; set; } = null!;
        /// <summary>
        /// ["<c>fiat_denoted_total</c>"] Fiat denoted total
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
        /// ["<c>title</c>"] Title
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>description</c>"] Description
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>label</c>"] Label
        /// </summary>
        [JsonPropertyName("label")]
        public string Label { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public CoinbaseQuantityReference Quantity { get; set; } = null!;
        /// <summary>
        /// ["<c>disclosure</c>"] Disclosure
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
        /// ["<c>title</c>"] Title
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>description</c>"] Description
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>link</c>"] Link
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
        /// ["<c>text</c>"] Text
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>url</c>"] Url
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
        /// ["<c>target_to_fiat</c>"] Target to fiat
        /// </summary>
        [JsonPropertyName("target_to_fiat")]
        public CoinbaseQuantityScale TargetToFiat { get; set; } = null!;
        /// <summary>
        /// ["<c>target_to_source</c>"] Target to source
        /// </summary>
        [JsonPropertyName("target_to_source")]
        public CoinbaseQuantityScale TargetToSource { get; set; } = null!;
        /// <summary>
        /// ["<c>source_to_fiat</c>"] Source to fiat
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
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public CoinbaseQuantityReference Quantity { get; set; } = null!;
        /// <summary>
        /// ["<c>scale</c>"] Scale
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
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>code</c>"] Code
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>message</c>"] Message
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>link</c>"] Link
        /// </summary>
        [JsonPropertyName("link")]
        public CoinbaseLink? Link { get; set; }
        /// <summary>
        /// ["<c>context</c>"] Context
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
        /// ["<c>details</c>"] Details
        /// </summary>
        [JsonPropertyName("details")]
        public string[] Details { get; set; } = Array.Empty<string>();

        /// <summary>
        /// ["<c>title</c>"] Title
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>link_text</c>"] Link text
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
        /// ["<c>code</c>"] Code
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>message</c>"] Message
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>error_code</c>"] Error code
        /// </summary>
        [JsonPropertyName("error_code")]
        public string ErrorCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>error_cta</c>"] Error cta
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
        /// ["<c>name</c>"] Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Quantity
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
        /// ["<c>free_trading_reset_date</c>"] Free trading reset date
        /// </summary>
        [JsonPropertyName("free_trading_reset_date")]
        public DateTime? FreeTradingResetDate { get; set; }
        /// <summary>
        /// ["<c>used_zero_fee_trading</c>"] Used zero-fee trading
        /// </summary>
        [JsonPropertyName("used_zero_fee_trading")]
        public CoinbaseQuantityReference UsedZeroFeeTrading { get; set; } = null!;
        /// <summary>
        /// ["<c>remaining_free_trading_volume</c>"] Remaining free trading volume
        /// </summary>
        [JsonPropertyName("remaining_free_trading_volume")]
        public CoinbaseQuantityReference RemainingFreeTradingVolume { get; set; } = null!;
        /// <summary>
        /// ["<c>max_free_trading_volume</c>"] Max free trading volume
        /// </summary>
        [JsonPropertyName("max_free_trading_volume")]
        public CoinbaseQuantityReference MaxFreeTradingVolume { get; set; } = null!;
        /// <summary>
        /// ["<c>has_benefit_cap</c>"] Has benefit cap
        /// </summary>
        [JsonPropertyName("has_benefit_cap")]
        public bool? HasBenifitCap { get; set; }
        /// <summary>
        /// ["<c>applied_subscription_benefit</c>"] Applied subscription benefit
        /// </summary>
        [JsonPropertyName("applied_subscription_benefit")]
        public bool? AppliedSubscriptionBenefit { get; set; }
        /// <summary>
        /// ["<c>fee_without_subscription_benefit</c>"] Fee without subscription benefit
        /// </summary>
        [JsonPropertyName("fee_without_subscription_benefit")]
        public CoinbaseQuantityReference FeeWithoutSubscriptionBenefit { get; set; } = null!;
        /// <summary>
        /// ["<c>payment_method_fee_without_subscription_benefit</c>"] Payment method fee without subscription benefit
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
        /// ["<c>applied_incentive</c>"] Applied incentive
        /// </summary>
        [JsonPropertyName("applied_incentive")]
        public bool AppliedIncentive { get; set; }
        /// <summary>
        /// ["<c>user_incentive_id</c>"] User incentive id
        /// </summary>
        [JsonPropertyName("user_incentive_id")]
        public string UserIncentiveId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>code_val</c>"] Promo code value
        /// </summary>
        [JsonPropertyName("code_val")]
        public string PromoCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>ends_at</c>"] End time
        /// </summary>
        [JsonPropertyName("ends_at")]
        public DateTime? EndsAt { get; set; }
        /// <summary>
        /// ["<c>fee_without_incentive</c>"] Fee without incentive
        /// </summary>
        [JsonPropertyName("fee_without_incentive")]
        public CoinbaseQuantityReference Quantity { get; set; } = null!;
        /// <summary>
        /// ["<c>redeemed</c>"] Redeemed
        /// </summary>
        [JsonPropertyName("redeemed")]
        public bool? Redeemed { get; set; }
    }
}
