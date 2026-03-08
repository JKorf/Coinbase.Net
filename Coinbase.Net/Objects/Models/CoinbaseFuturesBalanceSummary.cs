using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    [SerializationModel]
    internal record CoinbaseFuturesBalanceSummaryWrapper
    {
        /// <summary>
        /// ["<c>balance_summary</c>"] Balance summary
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
        /// ["<c>futures_buying_power</c>"] Futures buying power
        /// </summary>
        [JsonPropertyName("futures_buying_power")]
        public CoinbaseQuantityReference FuturesBuyingPower { get; set; } = null!;
        /// <summary>
        /// ["<c>total_usd_balance</c>"] Total usd balance
        /// </summary>
        [JsonPropertyName("total_usd_balance")]
        public CoinbaseQuantityReference TotalUsdBalance { get; set; } = null!;
        /// <summary>
        /// ["<c>cbi_usd_balance</c>"] Cbi usd balance
        /// </summary>
        [JsonPropertyName("cbi_usd_balance")]
        public CoinbaseQuantityReference CbiUsdBalance { get; set; } = null!;
        /// <summary>
        /// ["<c>cfm_usd_balance</c>"] Cfm usd balance
        /// </summary>
        [JsonPropertyName("cfm_usd_balance")]
        public CoinbaseQuantityReference CfmUsdBalance { get; set; } = null!;
        /// <summary>
        /// ["<c>total_open_orders_hold_amount</c>"] Total open orders hold quantity
        /// </summary>
        [JsonPropertyName("total_open_orders_hold_amount")]
        public CoinbaseQuantityReference TotalOpenOrdersHoldQuantity { get; set; } = null!;
        /// <summary>
        /// ["<c>unrealized_pnl</c>"] Unrealized pnl
        /// </summary>
        [JsonPropertyName("unrealized_pnl")]
        public CoinbaseQuantityReference UnrealizedPnl { get; set; } = null!;
        /// <summary>
        /// ["<c>daily_realized_pnl</c>"] Daily realized pnl
        /// </summary>
        [JsonPropertyName("daily_realized_pnl")]
        public CoinbaseQuantityReference DailyRealizedPnl { get; set; } = null!;
        /// <summary>
        /// ["<c>initial_margin</c>"] Initial margin
        /// </summary>
        [JsonPropertyName("initial_margin")]
        public CoinbaseQuantityReference InitialMargin { get; set; } = null!;
        /// <summary>
        /// ["<c>available_margin</c>"] Available margin
        /// </summary>
        [JsonPropertyName("available_margin")]
        public CoinbaseQuantityReference AvailableMargin { get; set; } = null!;
        /// <summary>
        /// ["<c>liquidation_threshold</c>"] Liquidation threshold
        /// </summary>
        [JsonPropertyName("liquidation_threshold")]
        public CoinbaseQuantityReference LiquidationThreshold { get; set; } = null!;
        /// <summary>
        /// ["<c>liquidation_buffer_amount</c>"] Liquidation buffer quantity
        /// </summary>
        [JsonPropertyName("liquidation_buffer_amount")]
        public CoinbaseQuantityReference LiquidationBufferQuantity { get; set; } = null!;
        /// <summary>
        /// ["<c>liquidation_buffer_percentage</c>"] Liquidation buffer percentage
        /// </summary>
        [JsonPropertyName("liquidation_buffer_percentage")]
        public decimal LiquidationBufferPercentage { get; set; }
        /// <summary>
        /// ["<c>intraday_margin_window_measure</c>"] Intraday margin window measure
        /// </summary>
        [JsonPropertyName("intraday_margin_window_measure")]
        public CoinbaseFuturesBalanceWindow? IntradayMarginWindowMeasure { get; set; }
        /// <summary>
        /// ["<c>overnight_margin_window_measure</c>"] Overnight margin window measure
        /// </summary>
        [JsonPropertyName("overnight_margin_window_measure")]
        public CoinbaseFuturesBalanceWindow? OvernightMarginWindowMeasure { get; set; }
    }
}
