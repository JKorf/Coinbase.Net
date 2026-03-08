using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    [SerializationModel]
    internal record CoinbaseFiatAssetWrapper
    {
        /// <summary>
        /// ["<c>data</c>"] Data
        /// </summary>
        [JsonPropertyName("data")]
        public CoinbaseFiatAsset[] Data { get; set; } = Array.Empty<CoinbaseFiatAsset>();
    }

    /// <summary>
    /// Fiat asset info
    /// </summary>
    [SerializationModel]
    public record CoinbaseFiatAsset
    {
        /// <summary>
        /// ["<c>id</c>"] Asset
        /// </summary>
        [JsonPropertyName("id")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name</c>"] Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>min_size</c>"] Min quantity
        /// </summary>
        [JsonPropertyName("min_size")]
        public decimal MinQuantity { get; set; }
    }


}
