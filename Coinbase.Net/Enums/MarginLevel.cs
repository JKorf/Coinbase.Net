using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Margin level
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginLevel>))]
    public enum MarginLevel
    {
        /// <summary>
        /// ["<c>MARGIN_LEVEL_TYPE_UNSPECIFIED</c>"] Unspecified
        /// </summary>
        [Map("MARGIN_LEVEL_TYPE_UNSPECIFIED")]
        Unspecified,
        /// <summary>
        /// ["<c>MARGIN_LEVEL_TYPE_BASE</c>"] Base level
        /// </summary>
        [Map("MARGIN_LEVEL_TYPE_BASE")]
        Base,
        /// <summary>
        /// ["<c>MARGIN_LEVEL_TYPE_WARNING</c>"] Warning
        /// </summary>
        [Map("MARGIN_LEVEL_TYPE_WARNING")]
        Warning,
        /// <summary>
        /// ["<c>MARGIN_LEVEL_TYPE_DANGER</c>"] Danger
        /// </summary>
        [Map("MARGIN_LEVEL_TYPE_DANGER")]
        Danger,
        /// <summary>
        /// ["<c>MARGIN_LEVEL_TYPE_LIQUIDATION</c>"] Liquidation
        /// </summary>
        [Map("MARGIN_LEVEL_TYPE_LIQUIDATION")]
        Liquidation,
    }
}
