using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    [SerializationModel]
    internal record CoinbaseFuturesBalanceSummaryWrapper
    {
        /// <summary>
        /// Balance summary
        /// </summary>
        [JsonPropertyName("balance_summary")]
        public CoinbaseFuturesBalanceSummary BalanceSummary { get; set; } = null!;
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    public record CoinbaseFuturesBalanceSummary
    {
        /// <summary>
        /// Futures buying power
        /// </summary>
        [JsonPropertyName("futures_buying_power")]
        public CoinbaseQuantityReference FuturesBuyingPower { get; set; } = null!;
        /// <summary>
        /// Total usd balance
        /// </summary>
        [JsonPropertyName("total_usd_balance")]
        public CoinbaseQuantityReference TotalUsdBalance { get; set; } = null!;
        /// <summary>
        /// Cbi usd balance
        /// </summary>
        [JsonPropertyName("cbi_usd_balance")]
        public CoinbaseQuantityReference CbiUsdBalance { get; set; } = null!;
        /// <summary>
        /// Cfm usd balance
        /// </summary>
        [JsonPropertyName("cfm_usd_balance")]
        public CoinbaseQuantityReference CfmUsdBalance { get; set; } = null!;
        /// <summary>
        /// Total open orders hold quantity
        /// </summary>
        [JsonPropertyName("total_open_orders_hold_amount")]
        public CoinbaseQuantityReference TotalOpenOrdersHoldQuantity { get; set; } = null!;
        /// <summary>
        /// Unrealized pnl
        /// </summary>
        [JsonPropertyName("unrealized_pnl")]
        public CoinbaseQuantityReference UnrealizedPnl { get; set; } = null!;
        /// <summary>
        /// Daily realized pnl
        /// </summary>
        [JsonPropertyName("daily_realized_pnl")]
        public CoinbaseQuantityReference DailyRealizedPnl { get; set; } = null!;
        /// <summary>
        /// Initial margin
        /// </summary>
        [JsonPropertyName("initial_margin")]
        public CoinbaseQuantityReference InitialMargin { get; set; } = null!;
        /// <summary>
        /// Available margin
        /// </summary>
        [JsonPropertyName("available_margin")]
        public CoinbaseQuantityReference AvailableMargin { get; set; } = null!;
        /// <summary>
        /// Liquidation threshold
        /// </summary>
        [JsonPropertyName("liquidation_threshold")]
        public CoinbaseQuantityReference LiquidationThreshold { get; set; } = null!;
        /// <summary>
        /// Liquidation buffer quantity
        /// </summary>
        [JsonPropertyName("liquidation_buffer_amount")]
        public CoinbaseQuantityReference LiquidationBufferQuantity { get; set; } = null!;
        /// <summary>
        /// Liquidation buffer percentage
        /// </summary>
        [JsonPropertyName("liquidation_buffer_percentage")]
        public decimal LiquidationBufferPercentage { get; set; }
        /// <summary>
        /// Intraday margin window measure
        /// </summary>
        [JsonPropertyName("intraday_margin_window_measure")]
        public CoinbaseFuturesBalanceWindow? IntradayMarginWindowMeasure { get; set; }
        /// <summary>
        /// Overnight margin window measure
        /// </summary>
        [JsonPropertyName("overnight_margin_window_measure")]
        public CoinbaseFuturesBalanceWindow? OvernightMarginWindowMeasure { get; set; }
    }
}
