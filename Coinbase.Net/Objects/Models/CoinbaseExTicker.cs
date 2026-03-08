using Coinbase.Net.Enums;
using Coinbase.Net.Objects.Internal;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Ticker info
    /// </summary>
    public record CoinbaseExTicker : CoinbaseSocketEvent
    {
        /// <summary>
        /// ["<c>sequence</c>"] Sequence
        /// </summary>
        [JsonPropertyName("sequence")]
        public long Sequence { get; set; }
        /// <summary>
        /// ["<c>product_id</c>"] Symbol
        /// </summary>
        [JsonPropertyName("product_id")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>price</c>"] Last trade price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? LastPrice { get; set; }
        /// <summary>
        /// ["<c>open_24h</c>"] Open price 24 hours ago
        /// </summary>
        [JsonPropertyName("open_24h")]
        public decimal? OpenPrice24H { get; set; }
        /// <summary>
        /// ["<c>volume_24h</c>"] Volume in last 24 hours
        /// </summary>
        [JsonPropertyName("volume_24h")]
        public decimal? Volume24H { get; set; }
        /// <summary>
        /// ["<c>volume_30d</c>"] Volume in last 30 days
        /// </summary>
        [JsonPropertyName("volume_30d")]
        public decimal? Volume30D { get; set; }
        /// <summary>
        /// ["<c>low_24h</c>"] Lowest price in last 24 hours
        /// </summary>
        [JsonPropertyName("low_24h")]
        public decimal? LowPrice24H { get; set; }
        /// <summary>
        /// ["<c>high_24h</c>"] Highest price in last 24 hours
        /// </summary>
        [JsonPropertyName("high_24h")]
        public decimal? HighPrice24H { get; set; }
        /// <summary>
        /// ["<c>best_bid</c>"] Best bid price
        /// </summary>
        [JsonPropertyName("best_bid")]
        public decimal? BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>best_bid_size</c>"] Best bid quantity
        /// </summary>
        [JsonPropertyName("best_bid_size")]
        public decimal? BestBidQuantity { get; set; }
        /// <summary>
        /// ["<c>best_ask</c>"] Best ask price
        /// </summary>
        [JsonPropertyName("best_ask")]
        public decimal? BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>best_ask_size</c>"] Best ask quantity
        /// </summary>
        [JsonPropertyName("best_ask_size")]
        public decimal? BestAskQuantity { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide LastTradeSide { get; set; }
        /// <summary>
        /// ["<c>trade_id</c>"] Last trade id
        /// </summary>
        [JsonPropertyName("trade_id")]
        public long LastTradeId { get; set; }
        /// <summary>
        /// ["<c>last_size</c>"] Last trade quantity
        /// </summary>
        [JsonPropertyName("last_size")]
        public decimal LastTradeQuantity { get; set; }
        /// <summary>
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
    }
}
