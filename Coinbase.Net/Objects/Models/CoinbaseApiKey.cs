using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Enums;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// API key info
    /// </summary>
    [SerializationModel]
    public record CoinbaseApiKey
    {
        /// <summary>
        /// Can view
        /// </summary>
        [JsonPropertyName("can_view")]
        public bool CanView { get; set; }
        /// <summary>
        /// Can trade
        /// </summary>
        [JsonPropertyName("can_trade")]
        public bool CanTrade { get; set; }
        /// <summary>
        /// Can transfer
        /// </summary>
        [JsonPropertyName("can_transfer")]
        public bool CanTransfer { get; set; }
        /// <summary>
        /// Portfolio uuid
        /// </summary>
        [JsonPropertyName("portfolio_uuid")]
        public string PortfolioUuid { get; set; } = string.Empty;
        /// <summary>
        /// Portfolio type
        /// </summary>
        [JsonPropertyName("portfolio_type")]
        public PortfolioType PortfolioType { get; set; }
    }


}
