using Coinbase.Net.Enums;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Order info
    /// </summary>
    public record CoinbaseOrderResult
    {
        /// <summary>
        /// Whether the call was successful
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

        /// <summary>
        /// Order configuration
        /// </summary>
        [JsonPropertyName("order_configuration")]
        public CoinbaseOrderConfiguration OrderConfiguration { get; set; } = null!;
    }

    /// <summary>
    /// Success response
    /// </summary>
    public record CoinbaseOrderSuccess
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("product_id")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("client_order_id")]
        public string ClientOrderId { get; set; } = string.Empty;
    }

    /// <summary>
    /// Error response
    /// </summary>
    public record CoinbaseOrderError
    {
        /// <summary>
        /// Error code
        /// </summary>
        [JsonPropertyName("error")]
        public string ErrorCode { get; set; } = string.Empty;
        /// <summary>
        /// Message
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// Error details
        /// </summary>
        [JsonPropertyName("error_details")]
        public string ErrorDetails { get; set; } = string.Empty;
        /// <summary>
        /// Preview failure reason
        /// </summary>
        [JsonPropertyName("preview_failure_reason")]
        public string? PreviewFailureReason { get; set; }
        /// <summary>
        /// New order failure reason
        /// </summary>
        [JsonPropertyName("new_order_failure_reason")]
        public string? OrderFailureReason { get; set; }

        [JsonInclude]
        [JsonPropertyName("edit_order_failure_reason")]
        internal string? EditFailureReason { set => OrderFailureReason = value; }
    }
}
