using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Underlying type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<UnderlyingType>))]
    public enum UnderlyingType
    {
        /// <summary>
        /// ["<c>EQUITY</c>"] Equity
        /// </summary>
        [Map("EQUITY")]
        Equity,
        /// <summary>
        /// ["<c>EQUITY</c>"] Equity ETF
        /// </summary>
        [Map("EQUITY_ETF")]
        EquityEtf,
        /// <summary>
        /// ["<c>SPOT</c>"] Spot
        /// </summary>
        [Map("SPOT")]
        Spot,
        /// <summary>
        /// ["<c>COMMOD</c>"] Commodity
        /// </summary>
        [Map("COMMOD")]
        Commodity,
        /// <summary>
        /// ["<c>INDEX</c>"] Index
        /// </summary>
        [Map("INDEX")]
        Index,
        /// <summary>
        /// ["<c>PREIPO</c>"] Pre-IPO
        /// </summary>
        [Map("PREIPO")]
        PreIpo
    }
}
