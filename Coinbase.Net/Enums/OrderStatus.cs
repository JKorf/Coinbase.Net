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
        /// ["<c>PENDING</c>"] Pending placement
        /// </summary>
        [Map("PENDING")]
        Pending,
        /// <summary>
        /// ["<c>OPEN</c>"] Open
        /// </summary>
        [Map("OPEN")]
        Open,
        /// <summary>
        /// ["<c>FILLED</c>"] Filled
        /// </summary>
        [Map("FILLED")]
        Filled,
        /// <summary>
        /// ["<c>CANCELLED</c>"] Canceled
        /// </summary>
        [Map("CANCELLED")]
        Canceled,
        /// <summary>
        /// ["<c>EXPIRED</c>"] Expired
        /// </summary>
        [Map("EXPIRED")]
        Expired,
        /// <summary>
        /// ["<c>FAILED</c>"] Failed
        /// </summary>
        [Map("FAILED")]
        Failed,
        /// <summary>
        /// ["<c>UNKNOWN_ORDER_STATUS</c>"] Unknown
        /// </summary>
        [Map("UNKNOWN_ORDER_STATUS")]
        Unknown,
        /// <summary>
        /// ["<c>QUEUED</c>"] Order is queued
        /// </summary>
        [Map("QUEUED")]
        Queued,
        /// <summary>
        /// ["<c>CANCEL_QUEUED</c>"] Cancel has been queued
        /// </summary>
        [Map("CANCEL_QUEUED")]
        CancelQueued
    }
}
