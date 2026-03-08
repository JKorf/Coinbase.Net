using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Fee info
    /// </summary>
    [SerializationModel]
    public record CoinbaseFeeInfo
    {
        /// <summary>
        /// ["<c>total_volume</c>"] Total volume
        /// </summary>
        [JsonPropertyName("total_volume")]
        public decimal TotalVolume { get; set; }
        /// <summary>
        /// ["<c>total_fees</c>"] Total fees
        /// </summary>
        [JsonPropertyName("total_fees")]
        public decimal TotalFees { get; set; }
        /// <summary>
        /// ["<c>fee_tier</c>"] Fee tier
        /// </summary>
        [JsonPropertyName("fee_tier")]
        public CoinbaseFeeTier FeeTier { get; set; } = null!;
        /// <summary>
        /// ["<c>margin_rate</c>"] Margin rate
        /// </summary>
        [JsonPropertyName("margin_rate")]
        public decimal? MarginRate { get; set; }
        /// <summary>
        /// ["<c>goods_and_services_tax</c>"] Goods and services tax
        /// </summary>
        [JsonPropertyName("goods_and_services_tax")]
        public CoinbaseTax? GoodsAndServicesTax { get; set; }
        /// <summary>
        /// ["<c>advanced_trade_only_volume</c>"] Advanced trade only volume
        /// </summary>
        [JsonPropertyName("advanced_trade_only_volume")]
        public decimal AdvancedTradeOnlyVolume { get; set; }
        /// <summary>
        /// ["<c>advanced_trade_only_fees</c>"] Advanced trade only fees
        /// </summary>
        [JsonPropertyName("advanced_trade_only_fees")]
        public decimal AdvancedTradeOnlyFees { get; set; }
        /// <summary>
        /// ["<c>coinbase_pro_volume</c>"] Coinbase pro volume
        /// </summary>
        [JsonPropertyName("coinbase_pro_volume")]
        public decimal CoinbaseProVolume { get; set; }
        /// <summary>
        /// ["<c>coinbase_pro_fees</c>"] Coinbase pro fees
        /// </summary>
        [JsonPropertyName("coinbase_pro_fees")]
        public decimal CoinbaseProFees { get; set; }
        /// <summary>
        /// ["<c>total_balance</c>"] Total balance
        /// </summary>
        [JsonPropertyName("total_balance")]
        public decimal? TotalBalance { get; set; }
        /// <summary>
        /// ["<c>has_promo_fee</c>"] Promo fee active
        /// </summary>
        [JsonPropertyName("has_promo_fee")]
        public bool HasPromoFee { get; set; }
        /// <summary>
        /// ["<c>has_cost_plus_commission</c>"] Has cost plus commission
        /// </summary>
        [JsonPropertyName("has_cost_plus_commission")]
        public bool HasCostPlusCommission { get; set; }
        /// <summary>
        /// ["<c>volume_breakdown</c>"] Volume breakdown
        /// </summary>
        [JsonPropertyName("volume_breakdown")]
        public CoinbaseVolumeBreakdown[] VolumeBreakdown { get; set; } = [];
    }

    /// <summary>
    /// Tier info
    /// </summary>
    [SerializationModel]
    public record CoinbaseFeeTier
    {
        /// <summary>
        /// ["<c>pricing_tier</c>"] Pricing tier
        /// </summary>
        [JsonPropertyName("pricing_tier")]
        public string PricingTier { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>usd_from</c>"] Usd from
        /// </summary>
        [JsonPropertyName("usd_from")]
        public decimal? UsdFrom { get; set; }
        /// <summary>
        /// ["<c>usd_to</c>"] Usd to
        /// </summary>
        [JsonPropertyName("usd_to")]
        public decimal? UsdTo { get; set; }
        /// <summary>
        /// ["<c>taker_fee_rate</c>"] Taker fee rate
        /// </summary>
        [JsonPropertyName("taker_fee_rate")]
        public decimal TakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>maker_fee_rate</c>"] Maker fee rate
        /// </summary>
        [JsonPropertyName("maker_fee_rate")]
        public decimal MakerFeeRate { get; set; }
        /// <summary>
        /// ["<c>aop_from</c>"] Lower bound (inclusive) of pricing tier in usd of total assets on platform.
        /// </summary>
        [JsonPropertyName("aop_from")]
        public decimal? AopFrom { get; set; }
        /// <summary>
        /// ["<c>aop_to</c>"] Upper bound (exclusive) of pricing tier in usd of total assets on platform.
        /// </summary>
        [JsonPropertyName("aop_to")]
        public decimal? AopTo { get; set; }
        /// <summary>
        /// ["<c>perps_vol_from</c>"] Perp volume from
        /// </summary>
        [JsonPropertyName("perps_vol_from")]
        public decimal? PerpsFrom { get; set; }
        /// <summary>
        /// ["<c>perps_vol_to</c>"] Perp volume to
        /// </summary>
        [JsonPropertyName("perps_vol_to")]
        public decimal? PerpsTo { get; set; }
        /// <summary>
        /// ["<c>futures_vol_from</c>"] Futures volume from
        /// </summary>
        [JsonPropertyName("futures_vol_from")]
        public decimal? FuturesFrom { get; set; }
        /// <summary>
        /// ["<c>futures_vol_to</c>"] Futures volume to
        /// </summary>
        [JsonPropertyName("futures_vol_to")]
        public decimal? FuturesTo { get; set; }
        /// <summary>
        /// ["<c>volume_types_and_range</c>"] Volume types and ranges
        /// </summary>
        [JsonPropertyName("volume_types_and_range")]
        public CoinbaseVolumeRange[] VolumeRanges { get; set; } = [];
    }

    /// <summary>
    /// Tax rate
    /// </summary>
    [SerializationModel]
    public record CoinbaseTax
    {
        /// <summary>
        /// ["<c>rate</c>"] Rate
        /// </summary>
        [JsonPropertyName("rate")]
        public decimal Rate { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Type
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }

    /// <summary>
    /// Volume breakdown
    /// </summary>
    public record CoinbaseVolumeBreakdown
    {
        /// <summary>
        /// ["<c>volume_type</c>"] Volume type
        /// </summary>
        [JsonPropertyName("volume_type")]
        public string VolumeType { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>volume</c>"] Volume
        /// </summary>
        [JsonPropertyName("volume")]
        public decimal Volume { get; set; }
    }

    /// <summary>
    /// Volume type range
    /// </summary>
    public record CoinbaseVolumeRange
    {
        /// <summary>
        /// ["<c>volume_types</c>"] Volume types
        /// </summary>
        [JsonPropertyName("volume_types")]
        public string[] VolumeTypes { get; set; } = [];
        /// <summary>
        /// ["<c>vol_from</c>"] Volume from
        /// </summary>
        [JsonPropertyName("vol_from")]
        public decimal? VolumeFrom { get; set; }
        /// <summary>
        /// ["<c>vol_to</c>"] Volume to
        /// </summary>
        [JsonPropertyName("vol_to")]
        public decimal? VolumeTo { get; set; }
    }
}
