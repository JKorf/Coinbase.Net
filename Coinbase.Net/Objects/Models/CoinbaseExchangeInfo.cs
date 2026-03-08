using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Exchange info
    /// </summary>
    public record CoinbaseExchangeInfo
    {
        /// <summary>
        /// ["<c>products</c>"] Symbol info
        /// </summary>
        [JsonPropertyName("products")]
        public CoinbaseExSymbol[] Symbols { get; set; } = [];
        /// <summary>
        /// ["<c>currencies</c>"] Asset info
        /// </summary>
        [JsonPropertyName("currencies")]
        public CoinbaseExAsset[] Assets { get; set; } = [];
    }
}
