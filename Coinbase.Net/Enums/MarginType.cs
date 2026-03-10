using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Margin type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginType>))]
    public enum MarginType
    {
        /// <summary>
        /// ["<c>MARGIN_TYPE_UNSPECIFIED</c>"] Unspecified
        /// </summary>
        [Map("MARGIN_TYPE_UNSPECIFIED", "UNKNOWN_MARGIN_TYPE")]
        Unspecified,
        /// <summary>
        /// ["<c>MARGIN_TYPE_CROSS</c>"] Cross margin
        /// </summary>
        [Map("MARGIN_TYPE_CROSS")]
        Cross,
        /// <summary>
        /// ["<c>MARGIN_TYPE_ISOLATED</c>"] Isolated margin
        /// </summary>
        [Map("MARGIN_TYPE_ISOLATED")]
        Isolated
    }
}
