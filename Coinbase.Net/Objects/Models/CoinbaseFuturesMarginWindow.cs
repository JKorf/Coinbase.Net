using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Current margin window info
    /// </summary>
    [SerializationModel]
    public record CoinbaseFuturesMarginWindow
    {
        /// <summary>
        /// ["<c>margin_window</c>"] Margin window
        /// </summary>
        [JsonPropertyName("margin_window")]
        public CoinbaseFuturesMarginWindowInfo MarginWindow { get; set; } = null!;
        /// <summary>
        /// ["<c>is_intraday_margin_killswitch_enabled</c>"] Is intraday margin killswitch enabled
        /// </summary>
        [JsonPropertyName("is_intraday_margin_killswitch_enabled")]
        public bool IsIntradayMarginKillswitchEnabled { get; set; }
        /// <summary>
        /// ["<c>is_intraday_margin_enrollment_killswitch_enabled</c>"] Is intraday margin enrollment killswitch enabled
        /// </summary>
        [JsonPropertyName("is_intraday_margin_enrollment_killswitch_enabled")]
        public bool IsIntradayMarginEnrollmentKillswitchEnabled { get; set; }
    }

    /// <summary>
    /// Window info
    /// </summary>
    [SerializationModel]
    public record CoinbaseFuturesMarginWindowInfo
    {
        /// <summary>
        /// ["<c>margin_window_type</c>"] Margin window type
        /// </summary>
        [JsonPropertyName("margin_window_type")]
        public MarginWindowType MarginWindowType { get; set; }
        /// <summary>
        /// ["<c>end_time</c>"] End time
        /// </summary>
        [JsonPropertyName("end_time")]
        public DateTime EndTime { get; set; }
    }


}
