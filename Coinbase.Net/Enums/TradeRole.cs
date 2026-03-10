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
        /// ["<c>UNKNOWN_LIQUIDITY_INDICATOR</c>"] Unknown role
        /// </summary>
        [Map("UNKNOWN_LIQUIDITY_INDICATOR")]
        Unknown,
        /// <summary>
        /// ["<c>TAKER</c>"] Taker
        /// </summary>
        [Map("TAKER")]
        Taker,
        /// <summary>
        /// ["<c>MAKER</c>"] Maker
        /// </summary>
        [Map("MAKER")]
        Maker
    }
}
