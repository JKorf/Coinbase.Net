using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Enums;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Position info
    /// </summary>
    [SerializationModel]
    public record CoinbasePerpetualPositionUpdate
    {
        /// <summary>
        /// ["<c>product_id</c>"] Symbol
        /// </summary>
        [JsonPropertyName("product_id")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>portfolio_uuid</c>"] Portfolio uuid
        /// </summary>
        [JsonPropertyName("portfolio_uuid")]
        public string PortfolioUuid { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>vwap</c>"] Volume weighted average price
        /// </summary>
        [JsonPropertyName("vwap")]
        public decimal VolumeWeightedAveragePrice { get; set; }
        /// <summary>
        /// ["<c>entry_vwap</c>"] Entry volume weighted average price
        /// </summary>
        [JsonPropertyName("entry_vwap")]
        public decimal EntryVolumeWeightedAveragePrice { get; set; }
        /// <summary>
        /// ["<c>position_side</c>"] Position side
        /// </summary>
        [JsonPropertyName("position_side")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// ["<c>margin_type</c>"] Margin type
        /// </summary>
        [JsonPropertyName("margin_type")]
        public MarginType MarginType { get; set; }
        /// <summary>
        /// ["<c>net_size</c>"] Net quantity
        /// </summary>
        [JsonPropertyName("net_size")]
        public decimal NetQuantity { get; set; }
        /// <summary>
        /// ["<c>buy_order_size</c>"] Buy order quantity
        /// </summary>
        [JsonPropertyName("buy_order_size")]
        public decimal BuyOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>sell_order_size</c>"] Sell order quantity
        /// </summary>
        [JsonPropertyName("sell_order_size")]
        public decimal SellOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// ["<c>mark_price</c>"] Mark price
        /// </summary>
        [JsonPropertyName("mark_price")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// ["<c>liquidation_price</c>"] Liquidation price
        /// </summary>
        [JsonPropertyName("liquidation_price")]
        public decimal LiquidationPrice { get; set; }
        /// <summary>
        /// 	 ["<c>im_notional</c>"] The amount this position contributes to the initial margin
        /// </summary>
        [JsonPropertyName("im_notional")]
        public decimal ImNotional { get; set; }
        /// <summary>
        /// ["<c>mm_notional</c>"] The amount this position contributes to the maintenance margin
        /// </summary>
        [JsonPropertyName("mm_notional")]
        public decimal MmNotional { get; set; }
        /// <summary>
        /// ["<c>position_notional</c>"] The notional value of the position
        /// </summary>
        [JsonPropertyName("position_notional")]
        public decimal PositionNotional { get; set; }
        /// <summary>
        /// ["<c>unrealized_pnl</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealized_pnl")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>aggregated_pnl</c>"] Aggregated profit and loss
        /// </summary>
        [JsonPropertyName("aggregated_pnl")]
        public decimal AggregatedPnl { get; set; }
    }


}
