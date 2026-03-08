using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Movement result
    /// </summary>
    [SerializationModel]
    public record CoinbasePortfolioMove
    {
        /// <summary>
        /// ["<c>source_portfolio_uuid</c>"] Source portfolio id
        /// </summary>
        [JsonPropertyName("source_portfolio_uuid")]
        public string SourcePortfolioId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>target_portfolio_uuid</c>"] Target portfolio id
        /// </summary>
        [JsonPropertyName("target_portfolio_uuid")]
        public string TargetPortfolioId { get; set; } = string.Empty;
    }
}
