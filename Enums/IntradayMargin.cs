using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Intraday margin setting
    /// </summary>
    public enum IntradayMargin
    {
        /// <summary>
        /// Unspecified
        /// </summary>
        [Map("INTRADAY_MARGIN_SETTING_UNSPECIFIED")]
        Unspecified,
        /// <summary>
        /// Standard
        /// </summary>
        [Map("INTRADAY_MARGIN_SETTING_STANDARD")]
        Standard,
        /// <summary>
        /// Intraday
        /// </summary>
        [Map("INTRADAY_MARGIN_SETTING_INTRADAY")]
        Intraday
    }
}
