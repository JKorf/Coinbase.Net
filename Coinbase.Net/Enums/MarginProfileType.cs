using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Profile type
    /// </summary>
    public enum MarginProfileType
    {
        /// <summary>
        /// Unspecified
        /// </summary>
        [Map("MARGIN_PROFILE_TYPE_UNSPECIFIED")]
        Unspecified,
        /// <summary>
        /// Regular
        /// </summary>
        [Map("MARGIN_PROFILE_TYPE_RETAIL_REGULAR")]
        Regular,
        /// <summary>
        /// Intraday margin 1
        /// </summary>
        [Map("MARGIN_PROFILE_TYPE_RETAIL_INTRADAY_MARGIN_1")]
        IntradayMargin1
    }
}
