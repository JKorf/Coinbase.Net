using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Paginated response
    /// </summary>
    [SerializationModel]
    public record CoinbasePaginatedResult<T>
    {
        /// <summary>
        /// Pagination
        /// </summary>
        [JsonPropertyName("pagination")]
        public CoinbasePageInfo Pagination { get; set; } = null!;
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public T[] Data { get; set; } = Array.Empty<T>();
    }

    /// <summary>
    /// Pagination info
    /// </summary>
    [SerializationModel]
    public record CoinbasePageInfo
    {
        /// <summary>
        /// To id
        /// </summary>
        [JsonPropertyName("ending_before")]
        public string? ToId { get; set; }
        /// <summary>
        /// From id
        /// </summary>
        [JsonPropertyName("starting_after")]
        public string? FromId { get; set; }
        /// <summary>
        /// Limit
        /// </summary>
        [JsonPropertyName("limit")]
        public decimal Limit { get; set; }
        /// <summary>
        /// Order
        /// </summary>
        [JsonPropertyName("order")]
        public string Order { get; set; } = string.Empty;
        /// <summary>
        /// Previous uri
        /// </summary>
        [JsonPropertyName("previous_uri")]
        public string? PreviousUri { get; set; }
        /// <summary>
        /// Next uri
        /// </summary>
        [JsonPropertyName("next_uri")]
        public string? NextUri { get; set; }
    }
}
