using Coinbase.Net.Objects.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Internal
{
    /// <summary>
    /// Socket update
    /// </summary>
    public record CoinbaseSocketEvent
    {
        /// <summary>
        /// Event type
        /// </summary>
        [JsonPropertyName("type")]
        public string EventType { get; set; } = string.Empty;
    }
}
