using System;
using System.Text.Json.Serialization;
using Coinbase.Net.Enums;
using CryptoExchange.Net.Converters.SystemTextJson;

namespace Coinbase.Net.Objects.Models
{

    [SerializationModel]
    internal record CoinbaseDepositWrapper
    {
        [JsonPropertyName("data")]
        public CoinbaseDeposit Data { get; set; } = null!;
    }

    /// <summary>
    /// Deposit info
    /// </summary>
    [SerializationModel]
    public record CoinbaseDeposit
    {
        /// <summary>
        /// ["<c>id</c>"] Deposit id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Status of deposit
        /// </summary>
        [JsonPropertyName("status")]
        public WithdrawalStatus Status { get; set; }
        /// <summary>
        /// ["<c>payment_method</c>"] Payment method
        /// </summary>
        [JsonPropertyName("payment_method")]
        public CoinbaseResourceReference PaymentMethod { get; set; } = null!;
        /// <summary>
        /// ["<c>transaction</c>"] Transaction
        /// </summary>
        [JsonPropertyName("transaction")]
        public CoinbaseResourceReference Transaction { get; set; } = null!;
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public CoinbaseQuantityReference Quantity { get; set; } = null!;
        /// <summary>
        /// ["<c>subtotal</c>"] Subtotal
        /// </summary>
        [JsonPropertyName("subtotal")]
        public CoinbaseQuantityReference Subtotal { get; set; } = null!;
        /// <summary>
        /// ["<c>created_at</c>"] Created at
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>updated_at</c>"] Updated at
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>resource</c>"] Resource
        /// </summary>
        [JsonPropertyName("resource")]
        public string Resource { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>resource_path</c>"] Resource path
        /// </summary>
        [JsonPropertyName("resource_path")]
        public string ResourcePath { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>committed</c>"] Committed
        /// </summary>
        [JsonPropertyName("committed")]
        public bool Committed { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public CoinbaseQuantityReference Fee { get; set; } = null!;
        /// <summary>
        /// ["<c>payout_at</c>"] Payout at
        /// </summary>
        [JsonPropertyName("payout_at")]
        public DateTime? PayoutAt { get; set; }
        /// <summary>
        /// ["<c>user_reference</c>"] User reference
        /// </summary>
        [JsonPropertyName("user_reference")]
        public string? UserReference { get; set; }
    }
}
