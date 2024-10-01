using Coinbase.Net.Enums;
using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    internal record CoinbaseWithdrawalWrapper
    {
        [JsonPropertyName("data")]
        public CoinbaseWithdrawal Data { get; set; } = null!;
    }

    /// <summary>
    /// Withdrawal info
    /// </summary>
    public record CoinbaseWithdrawal
    {
        /// <summary>
        /// Withdrawal id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Status of withdrawal
        /// </summary>
        [JsonPropertyName("status")]
        public WithdrawalStatus Status { get; set; }
        /// <summary>
        /// Payment method
        /// </summary>
        [JsonPropertyName("payment_method")]
        public CoinbaseResourceReference PaymentMethod { get; set; } = null!;
        /// <summary>
        /// Transaction
        /// </summary>
        [JsonPropertyName("transaction")]
        public CoinbaseResourceReference Transaction { get; set; } = null!;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public CoinbaseQuantityReference Quantity { get; set; } = null!;
        /// <summary>
        /// Subtotal
        /// </summary>
        [JsonPropertyName("subtotal")]
        public CoinbaseQuantityReference Subtotal { get; set; } = null!;
        /// <summary>
        /// Created at
        /// </summary>
        [JsonPropertyName("created_at")]
        public string CreateTime { get; set; } = string.Empty;
        /// <summary>
        /// Updated at
        /// </summary>
        [JsonPropertyName("updated_at")]
        public string UpdateTime { get; set; } = string.Empty;
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
        /// Committed
        /// </summary>
        [JsonPropertyName("committed")]
        public bool Committed { get; set; }
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public CoinbaseQuantityReference Fee { get; set; } = null!;
        /// <summary>
        /// Payout at
        /// </summary>
        [JsonPropertyName("payout_at")]
        public DateTime? PayoutAt { get; set; }
    }
}
