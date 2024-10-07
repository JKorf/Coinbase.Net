using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Status of an order
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Pending placement
        /// </summary>
        [Map("PENDING")]
        Pending,
        /// <summary>
        /// Open
        /// </summary>
        [Map("OPEN")]
        Open,
        /// <summary>
        /// Filled
        /// </summary>
        [Map("FILLED")]
        Filled,
        /// <summary>
        /// Canceled
        /// </summary>
        [Map("CANCELLED")]
        Canceled,
        /// <summary>
        /// Expired
        /// </summary>
        [Map("EXPIRED")]
        Expired,
        /// <summary>
        /// Failed
        /// </summary>
        [Map("FAILED")]
        Failed,
        /// <summary>
        /// Unknown
        /// </summary>
        [Map("UNKNOWN_ORDER_STATUS")]
        Unknown,
        /// <summary>
        /// Order is queued
        /// </summary>
        [Map("QUEUED")]
        Queued,
        /// <summary>
        /// Cancel has been queued
        /// </summary>
        [Map("CANCEL_QUEUED")]
        CancelQueued
    }
}
