using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Position side
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PositionSide>))]
    public enum PositionSide
    {
        /// <summary>
        /// Unspecified
        /// </summary>
        [Map("FUTURES_POSITION_SIDE_UNSPECIFIED", "POSITION_SIDE_UNKNOWN", "UNKNOWN")]
        Unspecified,
        /// <summary>
        /// Long position
        /// </summary>
        [Map("FUTURES_POSITION_SIDE_LONG", "POSITION_SIDE_LONG", "LONG")]
        Long,
        /// <summary>
        /// Short position
        /// </summary>
        [Map("FUTURES_POSITION_SIDE_SHORT", "POSITION_SIDE_SHORT", "SHORT")]
        Short
    }
}
