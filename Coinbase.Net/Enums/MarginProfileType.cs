using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Profile type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginProfileType>))]
    public enum MarginProfileType
    {
        /// <summary>
        /// Unspecified
        /// </summary>
        [Map("MARGIN_PROFILE_TYPE_UNSPECIFIED")]
        Unspecified,
        /// <summary>
        /// Regular
        /// </summary>
        [Map("MARGIN_PROFILE_TYPE_RETAIL_REGULAR")]
        Regular,
        /// <summary>
        /// Intraday margin 1
        /// </summary>
        [Map("MARGIN_PROFILE_TYPE_RETAIL_INTRADAY_MARGIN_1")]
        IntradayMargin1
    }
}
