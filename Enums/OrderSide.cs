using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

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
        [Map("BUY", "buy")]
        Buy,
        /// <summary>
        /// Sell
        /// </summary>
        [Map("SELL", "sell")]
        Sell
    }
}
