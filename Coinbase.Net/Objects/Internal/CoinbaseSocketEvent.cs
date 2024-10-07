using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Internal
{
    /// <summary>
    /// Socket update
    /// </summary>
    public record CoinbaseSocketEvent
    {
        /// <summary>
        /// Event type
        /// </summary>
        [JsonPropertyName("type")]
        public string EventType { get; set; } = string.Empty;
    }
}
