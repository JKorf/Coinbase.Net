using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Page result
    /// </summary>
    public record CoinbasePage
    {
        /// <summary>
        /// Has another page
        /// </summary>
        [JsonPropertyName("has_next")]
        public bool HasNextPage { get; set; }
        /// <summary>
        /// Next page cursor
        /// </summary>
        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }
        /// <summary>
        /// Total items
        /// </summary>
        [JsonPropertyName("size")]
        public int TotalItems { get; set; }
    }
}
