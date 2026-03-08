using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    [SerializationModel]
    internal record CoinbaseDepositAddressWrapper
    {
        /// <summary>
        /// ["<c>data</c>"] Data
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
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>address</c>"] Address
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name</c>"] Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>created_at</c>"] Created at
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>updated_at</c>"] Updated at
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// ["<c>network</c>"] Network
        /// </summary>
        [JsonPropertyName("network")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>resource</c>"] Resource
        /// </summary>
        [JsonPropertyName("resource")]
        public string Resource { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>resource_path</c>"] Resource path
        /// </summary>
        [JsonPropertyName("resource_path")]
        public string ResourcePath { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>destination_tag</c>"] Destination tag (for XRP etc)
        /// </summary>
        [JsonPropertyName("destination_tag")]
        public string? DestinationTag { get; set; }
    }


}
