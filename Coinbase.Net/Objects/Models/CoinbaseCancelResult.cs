using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    [SerializationModel]
    internal record CoinbaseCancelResultWrapper
    {
        [JsonPropertyName("results")]
        public CoinbaseCancelResult[] Results { get; set; } = Array.Empty<CoinbaseCancelResult>();
    }

    /// <summary>
    /// Cancel result
    /// </summary>
    [SerializationModel]
    public record CoinbaseCancelResult
    {
        /// <summary>
        /// ["<c>success</c>"] Success
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        /// <summary>
        /// ["<c>failure_reason</c>"] Error message
        /// </summary>
        [JsonPropertyName("failure_reason")]
        public string? ErrorMessage { get; set; }
        /// <summary>
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string? OrderId { get; set; }
    }


}
