using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Sort order
    /// </summary>
    public enum OrderSortBy
    {
        /// <summary>
        /// Unknown
        /// </summary>
        [Map("UNKNOWN_SORT_BY")]
        Unknown,
        /// <summary>
        /// Limit price
        /// </summary>
        [Map("LIMIT_PRICE")]
        Price,
        /// <summary>
        /// Last fill time
        /// </summary>
        [Map("LAST_FILL_TIME")]
        LastFillTime
    }
}
