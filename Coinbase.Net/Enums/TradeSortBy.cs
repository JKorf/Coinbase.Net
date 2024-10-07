using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Trade sort order
    /// </summary>
    public enum TradeSortBy
    {
        /// <summary>
        /// Unknown
        /// </summary>
        [Map("UNKNOWN_SORT_BY")]
        Unknown,
        /// <summary>
        /// Trade price
        /// </summary>
        [Map("PRICE")]
        Price,
        /// <summary>
        /// Trade time
        /// </summary>
        [Map("TRADE_TIME")]
        TradeTime
    }
}
