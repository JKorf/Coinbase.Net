using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Order edit acknowledgement
    /// </summary>
    [SerializationModel]
    public record CoinbaseEditOrderResult
    {
        /// <summary>
        /// ["<c>success</c>"] Whether the order edit request was placed
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        /// <summary>
        /// ["<c>errors</c>"] Reasons the edit request was rejected
        /// </summary>
        [JsonPropertyName("errors")]
        public CoinbaseOrderError[] Errors { get; set; } = Array.Empty<CoinbaseOrderError>();
    }
}
