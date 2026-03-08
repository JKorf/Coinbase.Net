using CryptoExchange.Net.Converters.SystemTextJson;
using System;
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
        /// ["<c>pagination</c>"] Pagination
        /// </summary>
        [JsonPropertyName("pagination")]
        public CoinbasePageInfo Pagination { get; set; } = null!;
        /// <summary>
        /// ["<c>data</c>"] Data
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
        /// ["<c>ending_before</c>"] To id
        /// </summary>
        [JsonPropertyName("ending_before")]
        public string? ToId { get; set; }
        /// <summary>
        /// ["<c>starting_after</c>"] From id
        /// </summary>
        [JsonPropertyName("starting_after")]
        public string? FromId { get; set; }
        /// <summary>
        /// ["<c>limit</c>"] Limit
        /// </summary>
        [JsonPropertyName("limit")]
        public decimal Limit { get; set; }
        /// <summary>
        /// ["<c>order</c>"] Order
        /// </summary>
        [JsonPropertyName("order")]
        public string Order { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>previous_uri</c>"] Previous uri
        /// </summary>
        [JsonPropertyName("previous_uri")]
        public string? PreviousUri { get; set; }
        /// <summary>
        /// ["<c>next_uri</c>"] Next uri
        /// </summary>
        [JsonPropertyName("next_uri")]
        public string? NextUri { get; set; }
    }
}
