﻿using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Margin flags
    /// </summary>
    public enum MarginFlags
    {
        /// <summary>
        /// Unspecified
        /// </summary>
        [Map("PORTFOLIO_MARGIN_FLAGS_UNSPECIFIED")]
        Unspecified,
        /// <summary>
        /// In liquidation
        /// </summary>
        [Map("PORTFOLIO_MARGIN_FLAGS_IN_LIQUIDATION")]
        InLiquidation
    }
}
