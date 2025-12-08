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
        /// Asset
        /// </summary>
        [JsonPropertyName("code")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Color
        /// </summary>
        [JsonPropertyName("color")]
        public string Color { get; set; } = string.Empty;
        /// <summary>
        /// Sort index
        /// </summary>
        [JsonPropertyName("sort_index")]
        public int SortIndex { get; set; }
        /// <summary>
        /// Exponent
        /// </summary>
        [JsonPropertyName("exponent")]
        public int Exponent { get; set; }
        /// <summary>
        /// Type of asset
        /// </summary>
        [JsonPropertyName("type")]
        public AssetType AssetType { get; set; }
        /// <summary>
        /// Address regex
        /// </summary>
        [JsonPropertyName("address_regex")]
        public string AddressRegex { get; set; } = string.Empty;
        /// <summary>
        /// Asset id
        /// </summary>
        [JsonPropertyName("asset_id")]
        public string AssetId { get; set; } = string.Empty;
    }


}
