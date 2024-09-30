using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Margin type
    /// </summary>
    public enum MarginType
    {
        /// <summary>
        /// Unspecified
        /// </summary>
        [Map("MARGIN_TYPE_UNSPECIFIED")]
        Unspecified,
        /// <summary>
        /// Cross margin
        /// </summary>
        [Map("MARGIN_TYPE_CROSS")]
        Cross,
        /// <summary>
        /// Isolated margin
        /// </summary>
        [Map("MARGIN_TYPE_ISOLATED")]
        Isolated
    }
}
