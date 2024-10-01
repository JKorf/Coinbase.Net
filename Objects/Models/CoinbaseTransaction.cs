using Coinbase.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    internal record CoinbaseTransactionWrapper
    {
        [JsonPropertyName("data")]
        public CoinbaseTransaction Data { get; set; } = null!;
    }

    /// <summary>
    /// Transaction info
    /// </summary>
    public record CoinbaseTransaction
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Type of the transaction
        /// </summary>
        [JsonPropertyName("type")]
        public TransactionType TransactionType { get; set; }
        /// <summary>
        /// Status of the transaction
        /// </summary>
        [JsonPropertyName("status")]
        public TransactionStatus TransactionStatus { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public CoinbaseQuantityReference Quantity { get; set; } = null!;
        /// <summary>
        /// Native quantity
        /// </summary>
        [JsonPropertyName("native_amount")]
        public CoinbaseQuantityReference NativeQuantity { get; set; } = null!;
        /// <summary>
        /// Description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        /// <summary>
        /// Created at
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Updated at
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// Resource
        /// </summary>
        [JsonPropertyName("resource")]
        public string Resource { get; set; } = string.Empty;
        /// <summary>
        /// Resource path
        /// </summary>
        [JsonPropertyName("resource_path")]
        public string ResourcePath { get; set; } = string.Empty;
        /// <summary>
        /// Details
        /// </summary>
        [JsonPropertyName("details")]
        public CoinbaseTransactionDetails Details { get; set; } = null!;
    }

    /// <summary>
    /// Transaction details
    /// </summary>
    public record CoinbaseTransactionDetails
    {
        /// <summary>
        /// Title
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Subtitle
        /// </summary>
        [JsonPropertyName("subtitle")]
        public string Subtitle { get; set; } = string.Empty;
    }


}
