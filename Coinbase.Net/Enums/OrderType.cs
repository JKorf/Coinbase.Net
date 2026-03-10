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
        /// ["<c>UNKNOWN_ORDER_TYPE</c>"] Unknown
        /// </summary>
        [Map("UNKNOWN_ORDER_TYPE")]
        Unknown,
        /// <summary>
        /// ["<c>MARKET</c>"] Market order
        /// </summary>
        [Map("MARKET")]
        Market,
        /// <summary>
        /// ["<c>LIMIT</c>"] Limit order
        /// </summary>
        [Map("LIMIT")]
        Limit,
        /// <summary>
        /// ["<c>STOP</c>"] Stop order
        /// </summary>
        [Map("STOP")]
        Stop,
        /// <summary>
        /// ["<c>STOP_LIMIT</c>"] Stop limit order
        /// </summary>
        [Map("STOP_LIMIT", "Stop Limit")]
        StopLimit,
        /// <summary>
        /// ["<c>BRACKET</c>"] Bracket order
        /// </summary>
        [Map("BRACKET")]
        Bracket,

    }
}
