using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Exchange info
    /// </summary>
    public record CoinbaseExchangeInfo
    {
        /// <summary>
        /// Symbol info
        /// </summary>
        [JsonPropertyName("products")]
        public CoinbaseExSymbol[] Symbols { get; set; } = [];
        /// <summary>
        /// Asset info
        /// </summary>
        [JsonPropertyName("currencies")]
        public CoinbaseExAsset[] Assets { get; set; } = [];
    }
}
