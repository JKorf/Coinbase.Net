using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Enums;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Futures balance info
    /// </summary>
    [SerializationModel]
    public record CoinbaseFuturesBalance
    {
        /// <summary>
        /// The amount of your cash balance that is available to trade CFM futures
        /// </summary>
        [JsonPropertyName("futures_buying_power")]
        public decimal CashAvailable { get; set; }
        /// <summary>
        /// Aggregate USD maintained across your CFTC-regulated futures account and your Coinbase Inc. spot account
        /// </summary>
        [JsonPropertyName("total_usd_balance")]
        public decimal TotalUsdBalance { get; set; }
        /// <summary>
        /// USD maintained in your Coinbase Inc. spot account
        /// </summary>
        [JsonPropertyName("cbi_usd_balance")]
        public decimal CbiUsdBalance { get; set; }
        /// <summary>
        /// USD maintained in your CFTC-regulated futures account. Funds held in your futures account are not available to trade spot
        /// </summary>
        [JsonPropertyName("cfm_usd_balance")]
        public decimal CfmUsdBalance { get; set; }
        /// <summary>
        /// Your total balance on hold for spot and futures open orders
        /// </summary>
        [JsonPropertyName("total_open_orders_hold_amount")]
        public decimal TotalOpenOrdersHoldQuantity { get; set; }
        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealized_pnl")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// Daily realized profit and loss
        /// </summary>
        [JsonPropertyName("daily_realized_pnl")]
        public decimal DailyRealizedPnl { get; set; }
        /// <summary>
        /// Initial margin
        /// </summary>
        [JsonPropertyName("initial_margin")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// Available margin
        /// </summary>
        [JsonPropertyName("available_margin")]
        public decimal AvailableMargin { get; set; }
        /// <summary>
        /// When your available funds for collateral drop to the liquidation threshold, some or all of your futures positions will be liquidated
        /// </summary>
        [JsonPropertyName("liquidation_threshold")]
        public decimal LiquidationThreshold { get; set; }
        /// <summary>
        /// Funds available in excess of the liquidation threshold, calculated as available margin minus liquidation threshold. If your liquidation buffer amount reaches 0, your futures positions and/or open orders will be liquidated as necessary
        /// </summary>
        [JsonPropertyName("liquidation_buffer_amount")]
        public decimal LiquidationBufferQuantity { get; set; }
        /// <summary>
        /// Funds available in excess of the liquidation threshold expressed as a percentage. If your liquidation buffer percentage reaches 0%, your futures positions and/or open orders will be liquidated as necessary
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

    /// <summary>
    /// Window info
    /// </summary>
    [SerializationModel]
    public record CoinbaseFuturesBalanceWindow
    {
        /// <summary>
        /// Margin window type
        /// </summary>
        [JsonPropertyName("margin_window_type")]
        public MarginWindowType MarginWindowType { get; set; }
        /// <summary>
        /// Margin level
        /// </summary>
        [JsonPropertyName("margin_level")]
        public MarginLevel MarginLevel { get; set; }
        /// <summary>
        /// Initial margin
        /// </summary>
        [JsonPropertyName("initial_margin")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// Maintenance margin
        /// </summary>
        [JsonPropertyName("maintenance_margin")]
        public decimal MaintenanceMargin { get; set; }
        /// <summary>
        /// Liquidation buffer percentage
        /// </summary>
        [JsonPropertyName("liquidation_buffer_percentage")]
        public decimal? LiquidationBufferPercentage { get; set; }
        /// <summary>
        /// Liquidation buffer
        /// </summary>
        [JsonPropertyName("liquidation_buffer")]
        public decimal? LiquidationBuffer { get; set; }
        /// <summary>
        /// Total hold
        /// </summary>
        [JsonPropertyName("total_hold")]
        public decimal TotalHold { get; set; }
        /// <summary>
        /// Futures buying power
        /// </summary>
        [JsonPropertyName("futures_buying_power")]
        public decimal FuturesBuyingPower { get; set; }
    }

}
