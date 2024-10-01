using Coinbase.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    internal record CoinbaseCryptoAssetWrapper
    {
        [JsonPropertyName("data")]
        public IEnumerable<CoinbaseCryptoAsset> Data { get; set; } = Array.Empty<CoinbaseCryptoAsset>();
    }

    /// <summary>
    /// Crypto asset
    /// </summary>
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
