using Coinbase.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    internal record CoinbasePortfoliosWrapper
    {
        [JsonPropertyName("portfolios")]
        public IEnumerable<CoinbasePortfolio> Portfolios { get; set; } = new List<CoinbasePortfolio>();
    }

    internal record CoinbasePortfolioWrapper
    {
        [JsonPropertyName("portfolio")]
        public CoinbasePortfolio Portfolio { get; set; } = null!;
    }

    /// <summary>
    /// Portfolio info
    /// </summary>
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
