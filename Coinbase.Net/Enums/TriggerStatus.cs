using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Trigger status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TriggerStatus>))]
    public enum TriggerStatus
    {
        /// <summary>
        /// ["<c>UNKNOWN_TRIGGER_STATUS</c>"] Unknown
        /// </summary>
        [Map("UNKNOWN_TRIGGER_STATUS")]
        Unknown,
        /// <summary>
        /// ["<c>INVALID_ORDER_TYPE</c>"] Invalid order type
        /// </summary>
        [Map("INVALID_ORDER_TYPE")]
        InvalidOrderType,
        /// <summary>
        /// ["<c>STOP_PENDING</c>"] Stop pending
        /// </summary>
        [Map("STOP_PENDING")]
        StopPending,
        /// <summary>
        /// ["<c>STOP_TRIGGERED</c>"] Stop triggered
        /// </summary>
        [Map("STOP_TRIGGERED")]
        StopTriggered
    }
}
