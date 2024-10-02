using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Heartbeat
    /// </summary>
    public record CoinbaseHeartbeat
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
