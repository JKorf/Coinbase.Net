using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Direction of a stop order trigger
    /// </summary>
    public enum StopDirection
    {
        /// <summary>
        /// Triggers when price is higher as the stop trigger price
        /// </summary>
        [Map("STOP_DIRECTION_STOP_UP")]
        Up,
        /// <summary>
        /// Triggers when price is lower as the stop trigger price
        /// </summary>
        [Map("STOP_DIRECTION_STOP_DOWN")]
        Down
    }
}
