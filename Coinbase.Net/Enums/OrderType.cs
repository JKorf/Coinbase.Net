using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderType>))]
    public enum OrderType
    {
        /// <summary>
        /// Unknown
        /// </summary>
        [Map("UNKNOWN_ORDER_TYPE")]
        Unknown,
        /// <summary>
        /// Market order
        /// </summary>
        [Map("MARKET")]
        Market,
        /// <summary>
        /// Limit order
        /// </summary>
        [Map("LIMIT")]
        Limit,
        /// <summary>
        /// Stop order
        /// </summary>
        [Map("STOP")]
        Stop,
        /// <summary>
        /// Stop limit order
        /// </summary>
        [Map("STOP_LIMIT", "Stop Limit")]
        StopLimit,
        /// <summary>
        /// Bracket order
        /// </summary>
        [Map("BRACKET")]
        Bracket,

    }
}
