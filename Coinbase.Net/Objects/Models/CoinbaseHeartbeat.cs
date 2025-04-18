using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Objects.Internal;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Heartbeat
    /// </summary>
    [SerializationModel]
    public record CoinbaseHeartbeat : CoinbaseSocketEvent
    {
        /// <summary>
        /// Counter
        /// </summary>
        [JsonPropertyName("heartbeat_counter")]
        public long Counter { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("current_time")]
        public string Timestamp { get; set; } = string.Empty;
    }
}
