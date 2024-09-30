using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Kline interval
    /// </summary>
    public enum KlineInterval
    {
        /// <summary>
        /// Unknown
        /// </summary>
        [Map("UNKNOWN_GRANULARITY")]
        Unknown = 0,
        /// <summary>
        /// 1m
        /// </summary>
        [Map("ONE_MINUTE")]
        OneMinute = 60,
        /// <summary>
        /// 5m
        /// </summary>
        [Map("FIVE_MINUTE")]
        FiveMinutes = 60 * 5,
        /// <summary>
        /// 15m
        /// </summary>
        [Map("FIFTEEN_MINUTE")]
        FifteenMinutes = 60 * 15,
        /// <summary>
        /// 30m
        /// </summary>
        [Map("THIRTY_MINUTE")]
        ThirtyMinutes = 60 * 30,
        /// <summary>
        /// 1h
        /// </summary>
        [Map("ONE_HOUR")]
        OneHour = 60 * 60,
        /// <summary>
        /// 2h
        /// </summary>
        [Map("TWO_HOUR")]
        TwoHour = 60 * 60,
        /// <summary>
        /// 6h
        /// </summary>
        [Map("SIX_HOUR")]
        SixHours = 60 * 60 * 6,
        /// <summary>
        /// 1d
        /// </summary>
        [Map("ONE_DAY")]
        OneDay = 60 * 60 * 24
    }
}
