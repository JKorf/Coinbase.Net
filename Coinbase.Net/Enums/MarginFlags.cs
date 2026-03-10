using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Margin flags
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MarginFlags>))]
    public enum MarginFlags
    {
        /// <summary>
        /// ["<c>PORTFOLIO_MARGIN_FLAGS_UNSPECIFIED</c>"] Unspecified
        /// </summary>
        [Map("PORTFOLIO_MARGIN_FLAGS_UNSPECIFIED")]
        Unspecified,
        /// <summary>
        /// ["<c>PORTFOLIO_MARGIN_FLAGS_IN_LIQUIDATION</c>"] In liquidation
        /// </summary>
        [Map("PORTFOLIO_MARGIN_FLAGS_IN_LIQUIDATION")]
        InLiquidation
    }
}
