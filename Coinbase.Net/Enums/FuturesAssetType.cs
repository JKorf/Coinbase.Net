using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Futures asset type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<FuturesAssetType>))]
    public enum FuturesAssetType
    {
        /// <summary>
        /// ["<c>FUTURES_ASSET_TYPE_METALS</c>"] Metals
        /// </summary>
        [Map("FUTURES_ASSET_TYPE_METALS")]
        Metals,
        /// <summary>
        /// ["<c>FUTURES_ASSET_TYPE_CRYPTO</c>"] Crypto
        /// </summary>
        [Map("FUTURES_ASSET_TYPE_CRYPTO")]
        Crypto,
        /// <summary>
        /// ["<c>FUTURES_ASSET_TYPE_STOCKS</c>"] Stocks
        /// </summary>
        [Map("FUTURES_ASSET_TYPE_STOCKS")]
        Stocks,
        /// <summary>
        /// ["<c>FUTURES_ASSET_TYPE_ENERGY</c>"] Energy
        /// </summary>
        [Map("FUTURES_ASSET_TYPE_ENERGY")]
        Energy
    }
}
