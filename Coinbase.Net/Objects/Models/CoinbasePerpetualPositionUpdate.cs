using Coinbase.Net.Enums;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Position info
    /// </summary>
    public record CoinbasePerpetualPositionUpdate
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("product_id")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Portfolio uuid
        /// </summary>
        [JsonPropertyName("portfolio_uuid")]
        public string PortfolioUuid { get; set; } = string.Empty;
        /// <summary>
        /// Volume weighted average price
        /// </summary>
        [JsonPropertyName("vwap")]
        public decimal VolumeWeightedAveragePrice { get; set; }
        /// <summary>
        /// Entry volume weighted average price
        /// </summary>
        [JsonPropertyName("entry_vwap")]
        public decimal EntryVolumeWeightedAveragePrice { get; set; }
        /// <summary>
        /// Position side
        /// </summary>
        [JsonPropertyName("position_side")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// Margin type
        /// </summary>
        [JsonPropertyName("margin_type")]
        public MarginType MarginType { get; set; }
        /// <summary>
        /// Net quantity
        /// </summary>
        [JsonPropertyName("net_size")]
        public decimal NetQuantity { get; set; }
        /// <summary>
        /// Buy order quantity
        /// </summary>
        [JsonPropertyName("buy_order_size")]
        public decimal BuyOrderQuantity { get; set; }
        /// <summary>
        /// Sell order quantity
        /// </summary>
        [JsonPropertyName("sell_order_size")]
        public decimal SellOrderQuantity { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// Mark price
        /// </summary>
        [JsonPropertyName("mark_price")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// Liquidation price
        /// </summary>
        [JsonPropertyName("liquidation_price")]
        public decimal LiquidationPrice { get; set; }
        /// <summary>
        /// 	 The amount this position contributes to the initial margin
        /// </summary>
        [JsonPropertyName("im_notional")]
        public decimal ImNotional { get; set; }
        /// <summary>
        /// The amount this position contributes to the maintenance margin
        /// </summary>
        [JsonPropertyName("mm_notional")]
        public decimal MmNotional { get; set; }
        /// <summary>
        /// The notional value of the position
        /// </summary>
        [JsonPropertyName("position_notional")]
        public decimal PositionNotional { get; set; }
        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealized_pnl")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// Aggregated profit and loss
        /// </summary>
        [JsonPropertyName("aggregated_pnl")]
        public decimal AggregatedPnl { get; set; }
    }


}
