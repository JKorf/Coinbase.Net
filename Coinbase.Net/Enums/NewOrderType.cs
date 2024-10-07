using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Order type
    /// </summary>
    public enum NewOrderType
    {
        /// <summary>
        /// Market order
        /// </summary>
        [Map("market_market_ioc")]
        Market,
        /// <summary>
        /// Limit order
        /// </summary>
        [Map("limit_limit_gtc")]
        Limit,
        /// <summary>
        /// Limit order, will only succeed if it is possible to fill at least a part of the order, any unfilled quantity is canceled
        /// </summary>
        [Map("sor_limit_ioc")]
        LimitImmediateOrCancel,
        /// <summary>
        /// Limit order, active until a certain date
        /// </summary>
        [Map("limit_limit_gtd")]
        LimitGoodTillDate,
        /// <summary>
        /// Limit order, will only succeed if it is to immediately and completely filled
        /// </summary>
        [Map("limit_limit_fok")]
        LimitFillOrKill,
        /// <summary>
        /// Stop order, active until canceled
        /// </summary>
        [Map("stop_limit_stop_limit_gtc")]
        StopLimit,
        /// <summary>
        /// Stop order, active until a certain date
        /// </summary>
        [Map("stop_limit_stop_limit_gtd")]
        StopLimitGoodTillDate,
        /// <summary>
        /// Limit order with stop limit order parameters embedded in the order, active until canceled
        /// </summary>
        [Map("trigger_bracket_gtc")]
        Bracket,
        /// <summary>
        /// Limit order with stop limit order parameters embedded in the order, active until a certain date
        /// </summary>
        [Map("trigger_bracket_gtd")]
        BracketGoodTillDate
    }
}
