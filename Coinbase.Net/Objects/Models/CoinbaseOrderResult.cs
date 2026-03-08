using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Enums;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Order info
    /// </summary>
    [SerializationModel]
    public record CoinbaseOrderResult
    {
        /// <summary>
        /// ["<c>success</c>"] Whether the call was succesfull
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        /// <summary>
        /// ["<c>success_response</c>"] Success response
        /// </summary>
        [JsonPropertyName("success_response")]
        public CoinbaseOrderSuccess SuccessResponse { get; set; } = null!;
        /// <summary>
        /// ["<c>error_response</c>"] Error response
        /// </summary>
        [JsonPropertyName("error_response")]
        public CoinbaseOrderError ErrorResponse { get; set; } = null!;

        /// <summary>
        /// ["<c>order_configuration</c>"] Order configuration
        /// </summary>
        [JsonPropertyName("order_configuration")]
        public CoinbaseOrderConfiguration OrderConfiguration { get; set; } = null!;
    }

    /// <summary>
    /// Success response
    /// </summary>
    [SerializationModel]
    public record CoinbaseOrderSuccess
    {
        /// <summary>
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>product_id</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("product_id")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>side</c>"] Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// ["<c>client_order_id</c>"] Client order id
        /// </summary>
        [JsonPropertyName("client_order_id")]
        public string ClientOrderId { get; set; } = string.Empty;
    }

    /// <summary>
    /// Error response
    /// </summary>
    [SerializationModel]
    public record CoinbaseOrderError
    {
        /// <summary>
        /// ["<c>error</c>"] Error code
        /// </summary>
        [JsonPropertyName("error")]
        public string ErrorCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>message</c>"] Message
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>error_details</c>"] Error details
        /// </summary>
        [JsonPropertyName("error_details")]
        public string ErrorDetails { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>preview_failure_reason</c>"] Preview failure reason
        /// </summary>
        [JsonPropertyName("preview_failure_reason")]
        public string? PreviewFailureReason { get; set; }
        /// <summary>
        /// ["<c>new_order_failure_reason</c>"] New order failure reason
        /// </summary>
        [JsonPropertyName("new_order_failure_reason")]
        public string? OrderFailureReason { get; set; }

        [JsonInclude]
        [JsonPropertyName("edit_order_failure_reason")]
        internal string? EditFailureReason { set => OrderFailureReason = value; }
    }
}
