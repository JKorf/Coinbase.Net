using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Status of an order
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderStatus>))]
    public enum OrderStatus
    {
        /// <summary>
        /// Pending placement
        /// </summary>
        [Map("PENDING")]
        Pending,
        /// <summary>
        /// Open
        /// </summary>
        [Map("OPEN")]
        Open,
        /// <summary>
        /// Filled
        /// </summary>
        [Map("FILLED")]
        Filled,
        /// <summary>
        /// Canceled
        /// </summary>
        [Map("CANCELLED")]
        Canceled,
        /// <summary>
        /// Expired
        /// </summary>
        [Map("EXPIRED")]
        Expired,
        /// <summary>
        /// Failed
        /// </summary>
        [Map("FAILED")]
        Failed,
        /// <summary>
        /// Unknown
        /// </summary>
        [Map("UNKNOWN_ORDER_STATUS")]
        Unknown,
        /// <summary>
        /// Order is queued
        /// </summary>
        [Map("QUEUED")]
        Queued,
        /// <summary>
        /// Cancel has been queued
        /// </summary>
        [Map("CANCEL_QUEUED")]
        CancelQueued
    }
}
