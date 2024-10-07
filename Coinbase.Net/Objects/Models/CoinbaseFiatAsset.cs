using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    internal record CoinbaseFiatAssetWrapper
    {
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public IEnumerable<CoinbaseFiatAsset> Data { get; set; } = Array.Empty<CoinbaseFiatAsset>();
    }

    /// <summary>
    /// Fiat asset info
    /// </summary>
    public record CoinbaseFiatAsset
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("id")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Min quantity
        /// </summary>
        [JsonPropertyName("min_size")]
        public decimal MinQuantity { get; set; }
    }


}
