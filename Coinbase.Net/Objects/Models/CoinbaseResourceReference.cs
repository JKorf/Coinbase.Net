using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{

    /// <summary>
    /// Resource reference
    /// </summary>
    [SerializationModel]
    public record CoinbaseResourceReference
    {
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>resource</c>"] Resource
        /// </summary>
        [JsonPropertyName("resource")]
        public string Resource { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>resource_path</c>"] Resource path
        /// </summary>
        [JsonPropertyName("resource_path")]
        public string? ResourcePath { get; set; }
    }

    /// <summary>
    /// Reference
    /// </summary>
    [SerializationModel]
    public record CoinbaseToReference : CoinbaseResourceReference
    {
        /// <summary>
        /// ["<c>address</c>"] Address
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; set; }

        /// <summary>
        /// ["<c>email</c>"] Email
        /// </summary>
        [JsonPropertyName("email")]
        public string? Email { get; set; }
    }
}
