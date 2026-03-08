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
        /// ["<c>can_view</c>"] Can view
        /// </summary>
        [JsonPropertyName("can_view")]
        public bool CanView { get; set; }
        /// <summary>
        /// ["<c>can_trade</c>"] Can trade
        /// </summary>
        [JsonPropertyName("can_trade")]
        public bool CanTrade { get; set; }
        /// <summary>
        /// ["<c>can_transfer</c>"] Can transfer
        /// </summary>
        [JsonPropertyName("can_transfer")]
        public bool CanTransfer { get; set; }
        /// <summary>
        /// ["<c>portfolio_uuid</c>"] Portfolio uuid
        /// </summary>
        [JsonPropertyName("portfolio_uuid")]
        public string PortfolioUuid { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>portfolio_type</c>"] Portfolio type
        /// </summary>
        [JsonPropertyName("portfolio_type")]
        public PortfolioType PortfolioType { get; set; }
    }


}
