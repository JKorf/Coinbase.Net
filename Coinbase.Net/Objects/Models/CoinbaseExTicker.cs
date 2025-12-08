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
        /// Sequence
        /// </summary>
        [JsonPropertyName("sequence")]
        public long Sequence { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("product_id")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Last trade price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? LastPrice { get; set; }
        /// <summary>
        /// Open price 24 hours ago
        /// </summary>
        [JsonPropertyName("open_24h")]
        public decimal? OpenPrice24H { get; set; }
        /// <summary>
        /// Volume in last 24 hours
        /// </summary>
        [JsonPropertyName("volume_24h")]
        public decimal? Volume24H { get; set; }
        /// <summary>
        /// Volume in last 30 days
        /// </summary>
        [JsonPropertyName("volume_30d")]
        public decimal? Volume30D { get; set; }
        /// <summary>
        /// Lowest price in last 24 hours
        /// </summary>
        [JsonPropertyName("low_24h")]
        public decimal? LowPrice24H { get; set; }
        /// <summary>
        /// Highest price in last 24 hours
        /// </summary>
        [JsonPropertyName("high_24h")]
        public decimal? HighPrice24H { get; set; }
        /// <summary>
        /// Best bid price
        /// </summary>
        [JsonPropertyName("best_bid")]
        public decimal? BestBidPrice { get; set; }
        /// <summary>
        /// Best bid quantity
        /// </summary>
        [JsonPropertyName("best_bid_size")]
        public decimal? BestBidQuantity { get; set; }
        /// <summary>
        /// Best ask price
        /// </summary>
        [JsonPropertyName("best_ask")]
        public decimal? BestAskPrice { get; set; }
        /// <summary>
        /// Best ask quantity
        /// </summary>
        [JsonPropertyName("best_ask_size")]
        public decimal? BestAskQuantity { get; set; }
        /// <summary>
        /// Side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide LastTradeSide { get; set; }
        /// <summary>
        /// Last trade id
        /// </summary>
        [JsonPropertyName("trade_id")]
        public long LastTradeId { get; set; }
        /// <summary>
        /// Last trade quantity
        /// </summary>
        [JsonPropertyName("last_size")]
        public decimal LastTradeQuantity { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
    }
}
