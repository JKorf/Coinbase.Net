using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Intraday margin setting
    /// </summary>
    [JsonConverter(typeof(EnumConverter<IntradayMargin>))]
    public enum IntradayMargin
    {
        /// <summary>
        /// Unspecified
        /// </summary>
        [Map("INTRADAY_MARGIN_SETTING_UNSPECIFIED")]
        Unspecified,
        /// <summary>
        /// Standard
        /// </summary>
        [Map("INTRADAY_MARGIN_SETTING_STANDARD")]
        Standard,
        /// <summary>
        /// Intraday
        /// </summary>
        [Map("INTRADAY_MARGIN_SETTING_INTRADAY")]
        Intraday
    }
}
