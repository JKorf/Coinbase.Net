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
        /// ["<c>FCM_MARGIN_WINDOW_TYPE_UNSPECIFIED</c>"] Unspecified
        /// </summary>
        [Map("FCM_MARGIN_WINDOW_TYPE_UNSPECIFIED", "MARGIN_WINDOW_TYPE_UNSPECIFIED")]
        Unspecified,
        /// <summary>
        /// ["<c>FCM_MARGIN_WINDOW_TYPE_OVERNIGHT</c>"] Overnight
        /// </summary>
        [Map("FCM_MARGIN_WINDOW_TYPE_OVERNIGHT", "MARGIN_WINDOW_TYPE_OVERNIGHT")]
        Overnight,
        /// <summary>
        /// ["<c>FCM_MARGIN_WINDOW_TYPE_WEEKEND</c>"] Weekend
        /// </summary>
        [Map("FCM_MARGIN_WINDOW_TYPE_WEEKEND", "MARGIN_WINDOW_TYPE_WEEKEND")]
        Weekend,
        /// <summary>
        /// ["<c>FCM_MARGIN_WINDOW_TYPE_INTRADAY</c>"] Intraday
        /// </summary>
        [Map("FCM_MARGIN_WINDOW_TYPE_INTRADAY", "MARGIN_WINDOW_TYPE_INTRADAY")]
        Intraday,
        /// <summary>
        /// ["<c>FCM_MARGIN_WINDOW_TYPE_TRANSITION</c>"] Transition
        /// </summary>
        [Map("FCM_MARGIN_WINDOW_TYPE_TRANSITION", "MARGIN_WINDOW_TYPE_TRANSITION")]
        Transition
    }
}
