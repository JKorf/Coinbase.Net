using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    [SerializationModel]
    internal record CoinbaseTransactionWrapper
    {
        [JsonPropertyName("data")]
        public CoinbaseTransaction Data { get; set; } = null!;
    }

    /// <summary>
    /// Transaction info
    /// </summary>
    [SerializationModel]
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
        /// Network info
        /// </summary>
        [JsonPropertyName("network")]
        public CoinbaseTransactionNetwork? Network { get; set; }
        /// <summary>
        /// Transfer target
        /// </summary>
        [JsonPropertyName("to")]
        public CoinbaseToReference? To { get; set; }
        /// <summary>
        /// Transfer source
        /// </summary>
        [JsonPropertyName("from")]
        public CoinbaseToReference? From { get; set; }
        /// <summary>
        /// Details
        /// </summary>
        [JsonPropertyName("details")]
        public CoinbaseTransactionDetails Details { get; set; } = null!;
    }

    /// <summary>
    /// Network info
    /// </summary>
    [SerializationModel]
    public record CoinbaseTransactionNetwork
    {
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Transaction hash, only provided when this is a SEND transaction
        /// </summary>
        [JsonPropertyName("hash")]
        public string? Hash { get; set; }
        /// <summary>
        /// Name of the network, only provided when this is a SEND transaction
        /// </summary>
        [JsonPropertyName("name")]
        public string? NetworkName { get; set; }

        /// <summary>
        /// Transaction fee, only provided when this is a SEND transaction
        /// </summary>
        [JsonPropertyName("transaction_fee")]
        public CoinbaseQuantityReference? TransactionFee { get; set; }

        /// <summary>
        /// Transaction quantity, only provided when this is a SEND transaction
        /// </summary>
        [JsonPropertyName("transaction_amount")]
        public CoinbaseQuantityReference? TransactionQuantity { get; set; }
    }

    /// <summary>
    /// Transaction details
    /// </summary>
    [SerializationModel]
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
