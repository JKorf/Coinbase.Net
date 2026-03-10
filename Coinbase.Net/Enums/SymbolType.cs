using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Symbol type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SymbolType>))]
    public enum SymbolType
    {
        /// <summary>
        /// ["<c>UNKNOWN_PRODUCT_TYPE</c>"] Unknown
        /// </summary>
        [Map("UNKNOWN_PRODUCT_TYPE")]
        Unknown,
        /// <summary>
        /// ["<c>SPOT</c>"] Spot symbol
        /// </summary>
        [Map("SPOT")]
        Spot,
        /// <summary>
        /// ["<c>FUTURE</c>"] Futures symbol
        /// </summary>
        [Map("FUTURE")]
        Futures
    }
}
