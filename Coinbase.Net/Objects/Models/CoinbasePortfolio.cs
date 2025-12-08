using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Enums;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    [SerializationModel]
    internal record CoinbasePortfoliosWrapper
    {
        [JsonPropertyName("portfolios")]
        public CoinbasePortfolio[] Portfolios { get; set; } = [];
    }

    [SerializationModel]
    internal record CoinbasePortfolioWrapper
    {
        [JsonPropertyName("portfolio")]
        public CoinbasePortfolio Portfolio { get; set; } = null!;
    }

    /// <summary>
    /// Portfolio info
    /// </summary>
    [SerializationModel]
    public record CoinbasePortfolio
    {
        /// <summary>
        /// Portfolio name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Portfolio id
        /// </summary>
        [JsonPropertyName("uuid")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Type
        /// </summary>
        [JsonPropertyName("type")]
        public PortfolioType PortfolioType { get; set; }
        /// <summary>
        /// Deleted
        /// </summary>
        [JsonPropertyName("deleted")]
        public bool Deleted { get; set; }
    }
}
