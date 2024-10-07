using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Order side
    /// </summary>
    public enum OrderSide
    {
        /// <summary>
        /// Buy
        /// </summary>
        [Map("BUY", "buy", "bid")]
        Buy,
        /// <summary>
        /// Sell
        /// </summary>
        [Map("SELL", "sell", "ask", "offer")]
        Sell,
        /// <summary>
        /// Unkown, only for data mapping
        /// </summary>
        [Map("UNKNOWN_ORDER_SIDE")]
        Unknown
    }
}
