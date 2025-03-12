using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Window type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginWindowType>))]
    public enum MarginWindowType
    {
        /// <summary>
        /// Unspecified
        /// </summary>
        [Map("FCM_MARGIN_WINDOW_TYPE_UNSPECIFIED", "MARGIN_WINDOW_TYPE_UNSPECIFIED")]
        Unspecified,
        /// <summary>
        /// Overnight
        /// </summary>
        [Map("FCM_MARGIN_WINDOW_TYPE_OVERNIGHT", "MARGIN_WINDOW_TYPE_OVERNIGHT")]
        Overnight,
        /// <summary>
        /// Weekend
        /// </summary>
        [Map("FCM_MARGIN_WINDOW_TYPE_WEEKEND", "MARGIN_WINDOW_TYPE_WEEKEND")]
        Weekend,
        /// <summary>
        /// Intraday
        /// </summary>
        [Map("FCM_MARGIN_WINDOW_TYPE_INTRADAY", "MARGIN_WINDOW_TYPE_INTRADAY")]
        Intraday,
        /// <summary>
        /// Transition
        /// </summary>
        [Map("FCM_MARGIN_WINDOW_TYPE_TRANSITION", "MARGIN_WINDOW_TYPE_TRANSITION")]
        Transition
    }
}
