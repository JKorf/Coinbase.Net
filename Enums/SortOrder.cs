using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Sort order
    /// </summary>
    public enum SortOrder
    {
        /// <summary>
        /// Ascending
        /// </summary>
        [Map("asc")]
        Ascending,
        /// <summary>
        /// Descending
        /// </summary>
        [Map("desc")]
        Descending
    }
}
