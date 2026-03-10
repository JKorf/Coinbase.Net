using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace Coinbase.Net.Enums
{
    /// <summary>
    /// Kline interval
    /// </summary>
    [JsonConverter(typeof(EnumConverter<KlineInterval>))]
    public enum KlineInterval
    {
        /// <summary>
        /// ["<c>UNKNOWN_GRANULARITY</c>"] Unknown
        /// </summary>
        [Map("UNKNOWN_GRANULARITY")]
        Unknown = 0,
        /// <summary>
        /// ["<c>ONE_MINUTE</c>"] 1m
        /// </summary>
        [Map("ONE_MINUTE")]
        OneMinute = 60,
        /// <summary>
        /// ["<c>FIVE_MINUTE</c>"] 5m
        /// </summary>
        [Map("FIVE_MINUTE")]
        FiveMinutes = 60 * 5,
        /// <summary>
        /// ["<c>FIFTEEN_MINUTE</c>"] 15m
        /// </summary>
        [Map("FIFTEEN_MINUTE")]
        FifteenMinutes = 60 * 15,
        /// <summary>
        /// ["<c>THIRTY_MINUTE</c>"] 30m
        /// </summary>
        [Map("THIRTY_MINUTE")]
        ThirtyMinutes = 60 * 30,
        /// <summary>
        /// ["<c>ONE_HOUR</c>"] 1h
        /// </summary>
        [Map("ONE_HOUR")]
        OneHour = 60 * 60,
        /// <summary>
        /// ["<c>TWO_HOUR</c>"] 2h
        /// </summary>
        [Map("TWO_HOUR")]
        TwoHours = 60 * 60 * 2,
        /// <summary>
        /// ["<c>SIX_HOUR</c>"] 6h
        /// </summary>
        [Map("SIX_HOUR")]
        SixHours = 60 * 60 * 6,
        /// <summary>
        /// ["<c>ONE_DAY</c>"] 1d
        /// </summary>
        [Map("ONE_DAY")]
        OneDay = 60 * 60 * 24
    }
}
