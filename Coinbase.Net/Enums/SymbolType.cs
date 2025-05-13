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
        /// Unknown
        /// </summary>
        [Map("UNKNOWN_PRODUCT_TYPE")]
        Unknown,
        /// <summary>
        /// Spot symbol
        /// </summary>
        [Map("SPOT")]
        Spot,
        /// <summary>
        /// Futures symbol
        /// </summary>
        [Map("FUTURE")]
        Futures
    }
}
