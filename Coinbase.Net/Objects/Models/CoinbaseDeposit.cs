using Coinbase.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{

    internal record CoinbaseDepositWrapper
    {
        [JsonPropertyName("data")]
        public CoinbaseDeposit Data { get; set; } = null!;
    }

    /// <summary>
    /// Deposit info
    /// </summary>
    public record CoinbaseDeposit
    {
        /// <summary>
        /// Deposit id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Status of deposit
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
