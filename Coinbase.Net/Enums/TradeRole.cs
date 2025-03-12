using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Trade role
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TradeRole>))]
    public enum TradeRole
    {
        /// <summary>
        /// Unknown role
        /// </summary>
        [Map("UNKNOWN_LIQUIDITY_INDICATOR")]
        Unknown,
        /// <summary>
        /// Taker
        /// </summary>
        [Map("TAKER")]
        Taker,
        /// <summary>
        /// Maker
        /// </summary>
        [Map("MAKER")]
        Maker
    }
}
