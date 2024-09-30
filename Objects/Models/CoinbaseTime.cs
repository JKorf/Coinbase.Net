using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Timestamp response
    /// </summary>
    public record CoinbaseTime
    {
        /// <summary>
        /// Current time
        /// </summary>
        [JsonPropertyName("epochMillis")]
        public DateTime Time { get; set; }
    }
}
