using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Fee info
    /// </summary>
    public record CoinbaseFeeInfo
    {
        /// <summary>
        /// Total volume
        /// </summary>
        [JsonPropertyName("total_volume")]
        public decimal TotalVolume { get; set; }
        /// <summary>
        /// Total fees
        /// </summary>
        [JsonPropertyName("total_fees")]
        public decimal TotalFees { get; set; }
        /// <summary>
        /// Fee tier
        /// </summary>
        [JsonPropertyName("fee_tier")]
        public CoinbaseFeeTier FeeTier { get; set; } = null!;
        /// <summary>
        /// Margin rate
        /// </summary>
        [JsonPropertyName("margin_rate")]
        public decimal? MarginRate { get; set; }
        /// <summary>
        /// Goods and services tax
        /// </summary>
        [JsonPropertyName("goods_and_services_tax")]
        public CoinbaseTax? GoodsAndServicesTax { get; set; }
        /// <summary>
        /// Advanced trade only volume
        /// </summary>
        [JsonPropertyName("advanced_trade_only_volume")]
        public decimal AdvancedTradeOnlyVolume { get; set; }
        /// <summary>
        /// Advanced trade only fees
        /// </summary>
        [JsonPropertyName("advanced_trade_only_fees")]
        public decimal AdvancedTradeOnlyFees { get; set; }
        /// <summary>
        /// Coinbase pro volume
        /// </summary>
        [JsonPropertyName("coinbase_pro_volume")]
        public decimal CoinbaseProVolume { get; set; }
        /// <summary>
        /// Coinbase pro fees
        /// </summary>
        [JsonPropertyName("coinbase_pro_fees")]
        public decimal CoinbaseProFees { get; set; }
        /// <summary>
        /// Total balance
        /// </summary>
        [JsonPropertyName("total_balance")]
        public decimal TotalBalance { get; set; }
        /// <summary>
        /// Promo fee active
        /// </summary>
        [JsonPropertyName("has_promo_fee")]
        public bool HasPromoFee { get; set; }
    }

    /// <summary>
    /// Tier info
    /// </summary>
    public record CoinbaseFeeTier
    {
        /// <summary>
        /// Pricing tier
        /// </summary>
        [JsonPropertyName("pricing_tier")]
        public string PricingTier { get; set; } = string.Empty;
        /// <summary>
        /// Usd from
        /// </summary>
        [JsonPropertyName("usd_from")]
        public decimal UsdFrom { get; set; }
        /// <summary>
        /// Usd to
        /// </summary>
        [JsonPropertyName("usd_to")]
        public decimal UsdTo { get; set; }
        /// <summary>
        /// Taker fee rate
        /// </summary>
        [JsonPropertyName("taker_fee_rate")]
        public decimal TakerFeeRate { get; set; }
        /// <summary>
        /// Maker fee rate
        /// </summary>
        [JsonPropertyName("maker_fee_rate")]
        public decimal MakerFeeRate { get; set; }
        /// <summary>
        /// Lower bound (inclusive) of pricing tier in usd of total assets on platform.
        /// </summary>
        [JsonPropertyName("aop_from")]
        public decimal? AopFrom { get; set; }
        /// <summary>
        /// Upper bound (exclusive) of pricing tier in usd of total assets on platform.
        /// </summary>
        [JsonPropertyName("aop_to")]
        public decimal? AopTo { get; set; }
    }

    /// <summary>
    /// Tax rate
    /// </summary>
    public record CoinbaseTax
    {
        /// <summary>
        /// Rate
        /// </summary>
        [JsonPropertyName("rate")]
        public decimal Rate { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }


}
