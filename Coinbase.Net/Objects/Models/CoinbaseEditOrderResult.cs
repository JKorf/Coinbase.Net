using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Order info
    /// </summary>
    public record CoinbaseEditOrderResult
    {
        /// <summary>
        /// Whether the call was succesfull
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Success response
        /// </summary>
        [JsonPropertyName("success_response")]
        public CoinbaseOrderSuccess SuccessResponse { get; set; } = null!;
        /// <summary>
        /// Error response
        /// </summary>
        [JsonPropertyName("error_response")]
        public CoinbaseOrderError ErrorResponse { get; set; } = null!;
    }
}
