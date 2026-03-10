using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Direction of a stop order trigger
    /// </summary>
    [JsonConverter(typeof(EnumConverter<StopDirection>))]
    public enum StopDirection
    {
        /// <summary>
        /// ["<c>STOP_DIRECTION_STOP_UP</c>"] Triggers when price is higher as the stop trigger price
        /// </summary>
        [Map("STOP_DIRECTION_STOP_UP")]
        Up,
        /// <summary>
        /// ["<c>STOP_DIRECTION_STOP_DOWN</c>"] Triggers when price is lower as the stop trigger price
        /// </summary>
        [Map("STOP_DIRECTION_STOP_DOWN")]
        Down
    }
}
