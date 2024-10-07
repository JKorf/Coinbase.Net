using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    internal record CoinbaseCancelResultWrapper
    {
        [JsonPropertyName("results")]
        public IEnumerable<CoinbaseCancelResult> Results { get; set; } = Array.Empty<CoinbaseCancelResult>();
    }

    /// <summary>
    /// Cancel result
    /// </summary>
    public record CoinbaseCancelResult
    {
        /// <summary>
        /// Success
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        /// <summary>
        /// Error message
        /// </summary>
        [JsonPropertyName("failure_reason")]
        public string? ErrorMessage { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string? OrderId { get; set; }
    }


}
