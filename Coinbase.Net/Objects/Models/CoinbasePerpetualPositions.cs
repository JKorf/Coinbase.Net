using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Enums;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    [SerializationModel]
    internal record CoinbasePerpetualPositionWrapper
    {
        [JsonPropertyName("position")]
        public CoinbasePerpetualPosition Position { get; set; } = null!;
    }

    /// <summary>
    /// Position summary
    /// </summary>
    [SerializationModel]
    public record CoinbasePerpetualPositions
    {
        /// <summary>
        /// ["<c>positions</c>"] Positions
        /// </summary>
        [JsonPropertyName("positions")]
        public CoinbasePerpetualPosition[] Positions { get; set; } = null!;
        /// <summary>
        /// ["<c>summary</c>"] Summary
        /// </summary>
        [JsonPropertyName("summary")]
        public CoinbasePerpetualPositionSummary Summary { get; set; } = null!;
    }

    /// <summary>
    /// Position info
    /// </summary>
    [SerializationModel]
    public record CoinbasePerpetualPosition
    {
        /// <summary>
        /// ["<c>product_id</c>"] Symbol
        /// </summary>
        [JsonPropertyName("product_id")]
        public string SymbolId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>product_uuid</c>"] Symbol id
        /// </summary>
        [JsonPropertyName("product_uuid")]
        public string SymbolUuid { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>portfolio_uuid</c>"] Portfolio id
        /// </summary>
        [JsonPropertyName("portfolio_uuid")]
        public string PorfolioId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>vwap</c>"] Volume weighted average price
        /// </summary>
        [JsonPropertyName("vwap")]
        public CoinbaseQuantityReference VolumeWeightedAveragePrice { get; set; } = null!;
        /// <summary>
        /// ["<c>entry_vwap</c>"] Entry volume weighted average price
        /// </summary>
        [JsonPropertyName("entry_vwap")]
        public CoinbaseQuantityReference EntryVolumeWeightedAveragePrice { get; set; } = null!;
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
        /// ["<c>net_size</c>"] Net quantity, negative value means short position, positive is long position
        /// </summary>
        [JsonPropertyName("net_size")]
        public decimal NetQuantity { get; set; }
        /// <summary>
        /// ["<c>buy_order_size</c>"] Cumulative size of all the open buy orders
        /// </summary>
        [JsonPropertyName("buy_order_size")]
        public decimal BuyOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>sell_order_size</c>"] Cumulative size of all the open sell orders
        /// </summary>
        [JsonPropertyName("sell_order_size")]
        public decimal SellOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>im_contribution</c>"] The amount this position contributes to the initial margin
        /// </summary>
        [JsonPropertyName("im_contribution")]
        public decimal InitialMarginContribution { get; set; }
        /// <summary>
        /// ["<c>unrealized_pnl</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("unrealized_pnl")]
        public CoinbaseQuantityReference UnrealizedPnl { get; set; } = null!;
        /// <summary>
        /// ["<c>mark_price</c>"] Mark price
        /// </summary>
        [JsonPropertyName("mark_price")]
        public CoinbaseQuantityReference MarkPrice { get; set; } = null!;
        /// <summary>
        /// ["<c>liquidation_price</c>"] Liquidation price
        /// </summary>
        [JsonPropertyName("liquidation_price")]
        public CoinbaseQuantityReference LiquidationPrice { get; set; } = null!;
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// ["<c>im_notional</c>"] Initial margin notional
        /// </summary>
        [JsonPropertyName("im_notional")]
        public CoinbaseQuantityReference InitialMarginNotional { get; set; } = null!;
        /// <summary>
        /// ["<c>mm_notional</c>"] Maintenance margin notional
        /// </summary>
        [JsonPropertyName("mm_notional")]
        public CoinbaseQuantityReference MaintenanceMarginNotional { get; set; } = null!;
        /// <summary>
        /// ["<c>position_notional</c>"] Position notional
        /// </summary>
        [JsonPropertyName("position_notional")]
        public CoinbaseQuantityReference PositionNotional { get; set; } = null!;
        /// <summary>
        /// ["<c>aggregated_pnl</c>"] Aggregated profit and loss
        /// </summary>
        [JsonPropertyName("aggregated_pnl")]
        public CoinbaseQuantityReference AggregatedPnl { get; set; } = null!;
    }

    /// <summary>
    /// Positions summary
    /// </summary>
    [SerializationModel]
    public record CoinbasePerpetualPositionSummary
    {
        /// <summary>
        /// ["<c>aggregated_pnl</c>"] Aggregated profit and loss
        /// </summary>
        [JsonPropertyName("aggregated_pnl")]
        public CoinbaseQuantityReference AggregatedPnl { get; set; } = null!;
    }
}
