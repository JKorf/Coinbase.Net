using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    [SerializationModel]
    internal record CoinbaseCryptoAssetWrapper
    {
        [JsonPropertyName("data")]
        public CoinbaseCryptoAsset[] Data { get; set; } = Array.Empty<CoinbaseCryptoAsset>();
    }

    /// <summary>
    /// Crypto asset
    /// </summary>
    [SerializationModel]
    public record CoinbaseCryptoAsset
    {
        /// <summary>
        /// ["<c>code</c>"] Asset
        /// </summary>
        [JsonPropertyName("code")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>name</c>"] Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>color</c>"] Color
        /// </summary>
        [JsonPropertyName("color")]
        public string Color { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>sort_index</c>"] Sort index
        /// </summary>
        [JsonPropertyName("sort_index")]
        public int SortIndex { get; set; }
        /// <summary>
        /// ["<c>exponent</c>"] Exponent
        /// </summary>
        [JsonPropertyName("exponent")]
        public int Exponent { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Type of asset
        /// </summary>
        [JsonPropertyName("type")]
        public AssetType AssetType { get; set; }
        /// <summary>
        /// ["<c>address_regex</c>"] Address regex
        /// </summary>
        [JsonPropertyName("address_regex")]
        public string AddressRegex { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>asset_id</c>"] Asset id
        /// </summary>
        [JsonPropertyName("asset_id")]
        public string AssetId { get; set; } = string.Empty;
    }


}
