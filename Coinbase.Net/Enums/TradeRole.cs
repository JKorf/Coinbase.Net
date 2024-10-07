using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Trade role
    /// </summary>
    public enum TradeRole
    {
        /// <summary>
        /// Unknown role
        /// </summary>
        [Map("UNKNOWN_LIQUIDITY_INDICATOR")]
        Unknown,
        /// <summary>
        /// Taker
        /// </summary>
        [Map("TAKER")]
        Taker,
        /// <summary>
        /// Maker
        /// </summary>
        [Map("MAKER")]
        Maker
    }
}
