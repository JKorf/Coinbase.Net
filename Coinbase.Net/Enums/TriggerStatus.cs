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
        /// Unknown
        /// </summary>
        [Map("UNKNOWN_TRIGGER_STATUS")]
        Unknown,
        /// <summary>
        /// Invalid order type
        /// </summary>
        [Map("INVALID_ORDER_TYPE")]
        InvalidOrderType,
        /// <summary>
        /// Stop pending
        /// </summary>
        [Map("STOP_PENDING")]
        StopPending,
        /// <summary>
        /// Stop triggered
        /// </summary>
        [Map("STOP_TRIGGERED")]
        StopTriggered
    }
}
