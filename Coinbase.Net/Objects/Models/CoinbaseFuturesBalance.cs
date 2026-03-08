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
        /// ["<c>futures_buying_power</c>"] The amount of your cash balance that is available to trade CFM futures
        /// </summary>
        [JsonPropertyName("futures_buying_power")]
        public decimal CashAvailable { get; set; }
        /// <summary>
        /// ["<c>total_usd_balance</c>"] Aggregate USD maintained across your CFTC-regulated futures account and your Coinbase Inc. spot account
        /// </summary>
        [JsonPropertyName("total_usd_balance")]
        public decimal TotalUsdBalance { get; set; }
        /// <summary>
        /// ["<c>cbi_usd_balance</c>"] USD maintained in your Coinbase Inc. spot account
        /// </summary>
        [JsonPropertyName("cbi_usd_balance")]
        public decimal CbiUsdBalance { get; set; }
        /// <summary>
        /// ["<c>cfm_usd_balance</c>"] USD maintained in your CFTC-regulated futures account. Funds held in your futures account are not available to trade spot
        /// </summary>
        [JsonPropertyName("cfm_usd_balance")]
        public decimal CfmUsdBalance { get; set; }
        /// <summary>
        /// ["<c>total_open_orders_hold_amount</c>"] Your total balance on hold for spot and futures open orders
        /// </summary>
        [JsonPropertyName("total_open_orders_hold_amount")]
        public decimal TotalOpenOrdersHoldQuantity { get; set; }
        /// <summary>
        /// ["<c>unrealized_pnl</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealized_pnl")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>daily_realized_pnl</c>"] Daily realized profit and loss
        /// </summary>
        [JsonPropertyName("daily_realized_pnl")]
        public decimal DailyRealizedPnl { get; set; }
        /// <summary>
        /// ["<c>initial_margin</c>"] Initial margin
        /// </summary>
        [JsonPropertyName("initial_margin")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// ["<c>available_margin</c>"] Available margin
        /// </summary>
        [JsonPropertyName("available_margin")]
        public decimal AvailableMargin { get; set; }
        /// <summary>
        /// ["<c>liquidation_threshold</c>"] When your available funds for collateral drop to the liquidation threshold, some or all of your futures positions will be liquidated
        /// </summary>
        [JsonPropertyName("liquidation_threshold")]
        public decimal LiquidationThreshold { get; set; }
        /// <summary>
        /// ["<c>liquidation_buffer_amount</c>"] Funds available in excess of the liquidation threshold, calculated as available margin minus liquidation threshold. If your liquidation buffer amount reaches 0, your futures positions and/or open orders will be liquidated as necessary
        /// </summary>
        [JsonPropertyName("liquidation_buffer_amount")]
        public decimal LiquidationBufferQuantity { get; set; }
        /// <summary>
        /// ["<c>liquidation_buffer_percentage</c>"] Funds available in excess of the liquidation threshold expressed as a percentage. If your liquidation buffer percentage reaches 0%, your futures positions and/or open orders will be liquidated as necessary
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

    /// <summary>
    /// Window info
    /// </summary>
    [SerializationModel]
    public record CoinbaseFuturesBalanceWindow
    {
        /// <summary>
        /// ["<c>margin_window_type</c>"] Margin window type
        /// </summary>
        [JsonPropertyName("margin_window_type")]
        public MarginWindowType MarginWindowType { get; set; }
        /// <summary>
        /// ["<c>margin_level</c>"] Margin level
        /// </summary>
        [JsonPropertyName("margin_level")]
        public MarginLevel MarginLevel { get; set; }
        /// <summary>
        /// ["<c>initial_margin</c>"] Initial margin
        /// </summary>
        [JsonPropertyName("initial_margin")]
        public decimal InitialMargin { get; set; }
        /// <summary>
        /// ["<c>maintenance_margin</c>"] Maintenance margin
        /// </summary>
        [JsonPropertyName("maintenance_margin")]
        public decimal? MaintenanceMargin { get; set; }
        /// <summary>
        /// ["<c>liquidation_buffer_percentage</c>"] Liquidation buffer percentage
        /// </summary>
        [JsonPropertyName("liquidation_buffer_percentage")]
        public decimal? LiquidationBufferPercentage { get; set; }
        /// <summary>
        /// ["<c>liquidation_buffer</c>"] Liquidation buffer
        /// </summary>
        [JsonPropertyName("liquidation_buffer")]
        public decimal? LiquidationBuffer { get; set; }
        /// <summary>
        /// ["<c>total_hold</c>"] Total hold
        /// </summary>
        [JsonPropertyName("total_hold")]
        public decimal? TotalHold { get; set; }
        /// <summary>
        /// ["<c>futures_buying_power</c>"] Futures buying power
        /// </summary>
        [JsonPropertyName("futures_buying_power")]
        public decimal? FuturesBuyingPower { get; set; }
    }

}
