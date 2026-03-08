using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Objects.Internal;
using System.Text.Json.Serialization;
using System;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Heartbeat
    /// </summary>
    [SerializationModel]
    public record CoinbaseExHeartbeat : CoinbaseSocketEvent
    {
        /// <summary>
        /// ["<c>product_id</c>"] Symbol
        /// </summary>
        [JsonPropertyName("product_id")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>sequence</c>"] Counter
        /// </summary>
        [JsonPropertyName("sequence")]
        public long Counter { get; set; }
        /// <summary>
        /// ["<c>last_trade_id</c>"] Last trade id
        /// </summary>
        [JsonPropertyName("last_trade_id")]
        public long LastTradeId { get; set; }
        /// <summary>
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
    }
}
