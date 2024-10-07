using Coinbase.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Current margin window info
    /// </summary>
    public record CoinbaseFuturesMarginWindow
    {
        /// <summary>
        /// Margin window
        /// </summary>
        [JsonPropertyName("margin_window")]
        public CoinbaseFuturesMarginWindowInfo MarginWindow { get; set; } = null!;
        /// <summary>
        /// Is intraday margin killswitch enabled
        /// </summary>
        [JsonPropertyName("is_intraday_margin_killswitch_enabled")]
        public bool IsIntradayMarginKillswitchEnabled { get; set; }
        /// <summary>
        /// Is intraday margin enrollment killswitch enabled
        /// </summary>
        [JsonPropertyName("is_intraday_margin_enrollment_killswitch_enabled")]
        public bool IsIntradayMarginEnrollmentKillswitchEnabled { get; set; }
    }

    /// <summary>
    /// Window info
    /// </summary>
    public record CoinbaseFuturesMarginWindowInfo
    {
        /// <summary>
        /// Margin window type
        /// </summary>
        [JsonPropertyName("margin_window_type")]
        public MarginWindowType MarginWindowType { get; set; }
        /// <summary>
        /// End time
        /// </summary>
        [JsonPropertyName("end_time")]
        public DateTime EndTime { get; set; }
    }


}
