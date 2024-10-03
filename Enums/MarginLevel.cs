using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Margin level
    /// </summary>
    public enum MarginLevel
    {
        /// <summary>
        /// Unspecified
        /// </summary>
        [Map("MARGIN_LEVEL_TYPE_UNSPECIFIED")]
        Unspecified,
        /// <summary>
        /// Base level
        /// </summary>
        [Map("MARGIN_LEVEL_TYPE_BASE")]
        Base,
        /// <summary>
        /// Warning
        /// </summary>
        [Map("MARGIN_LEVEL_TYPE_WARNING")]
        Warning,
        /// <summary>
        /// Danger
        /// </summary>
        [Map("MARGIN_LEVEL_TYPE_DANGER")]
        Danger,
        /// <summary>
        /// Liquidation
        /// </summary>
        [Map("MARGIN_LEVEL_TYPE_LIQUIDATION")]
        Liquidation,
    }
}
