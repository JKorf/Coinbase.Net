using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    [SerializationModel]
    internal record CoinbaseDepositAddressWrapper
    {
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public CoinbaseDepositAddress Data { get; set; } = null!;
    }

    /// <summary>
    /// Deposit address info
    /// </summary>
    [SerializationModel]
    public record CoinbaseDepositAddress
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Created at
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Updated at
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// Network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Resource
        /// </summary>
        [JsonPropertyName("resource")]
        public string Resource { get; set; } = string.Empty;
        /// <summary>
        /// Resource path
        /// </summary>
        [JsonPropertyName("resource_path")]
        public string ResourcePath { get; set; } = string.Empty;

        /// <summary>
        /// Destination tag (for XRP etc)
        /// </summary>
        [JsonPropertyName("destination_tag")]
        public string? DestinationTag { get; set; }
    }


}
