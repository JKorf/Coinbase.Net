using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Type of asset
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AssetType>))]
    public enum AssetType
    {
        /// <summary>
        /// ["<c>fiat</c>"] Fiat asset
        /// </summary>
        [Map("fiat")]
        Fiat,
        /// <summary>
        /// ["<c>crypto</c>"] Crypto asset
        /// </summary>
        [Map("crypto")]
        Crypto
    }
}
