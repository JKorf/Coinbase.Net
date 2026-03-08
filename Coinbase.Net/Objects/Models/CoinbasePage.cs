using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Page result
    /// </summary>
    [SerializationModel]
    public record CoinbasePage
    {
        /// <summary>
        /// ["<c>has_next</c>"] Has another page
        /// </summary>
        [JsonPropertyName("has_next")]
        public bool HasNextPage { get; set; }
        /// <summary>
        /// ["<c>cursor</c>"] Next page cursor
        /// </summary>
        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }
        /// <summary>
        /// ["<c>size</c>"] Total items
        /// </summary>
        [JsonPropertyName("size")]
        public int TotalItems { get; set; }
    }
}
