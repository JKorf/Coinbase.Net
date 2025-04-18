using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Timestamp response
    /// </summary>
    [SerializationModel]
    public record CoinbaseTime
    {
        /// <summary>
        /// Current time
        /// </summary>
        [JsonPropertyName("epochMillis")]
        public DateTime Time { get; set; }
    }
}
