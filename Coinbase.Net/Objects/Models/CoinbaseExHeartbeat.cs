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
        /// Symbol
        /// </summary>
        [JsonPropertyName("product_id")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Counter
        /// </summary>
        [JsonPropertyName("sequence")]
        public long Counter { get; set; }
        /// <summary>
        /// Last trade id
        /// </summary>
        [JsonPropertyName("last_trade_id")]
        public long LastTradeId { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
    }
}
