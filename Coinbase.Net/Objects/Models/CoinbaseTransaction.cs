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
        /// <summary>
        /// Trade info
        /// </summary>
        [JsonPropertyName("trade")]
        public CoinbaseTransactionTrade? Trade { get; set; }
        /// <summary>
        /// Buy info
        /// </summary>
        [JsonPropertyName("buy")]
        public CoinbaseBuyOrSell? Buy { get; set; }
        /// <summary>
        /// Sell info
        /// </summary>
        [JsonPropertyName("sell")]
        public CoinbaseBuyOrSell? Sell { get; set; }
        /// <summary>
        /// Advanced trade fill info
        /// </summary>
        [JsonPropertyName("advanced_trade_fill")]
        public CoinbaseAdvancedTradeFill? AdvancedTradeFill { get; set; }
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

    /// <summary>
    /// Trade details
    /// </summary>
    [SerializationModel]
    public record CoinbaseTransactionTrade
    {
        /// <summary>
        /// Id of the trade
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// The name of the payment method used for the trade
        /// </summary>
        [JsonPropertyName("payment_method_name")]
        public string PaymentMethodName { get; set; } = string.Empty;
    }

    /// <summary>
    /// Advanced trade fill details
    /// </summary>
    [SerializationModel]
    public record CoinbaseAdvancedTradeFill
    {
        /// <summary>
        /// Commission
        /// </summary>
        [JsonPropertyName("commission")]
        public decimal Commission { get; set; }
        /// <summary>
        /// Fill price
        /// </summary>
        [JsonPropertyName("fill_price")]
        public decimal FillPrice { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Order side (buy/sell)
        /// </summary>
        [JsonPropertyName("order_side")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// Product id
        /// </summary>
        [JsonPropertyName("product_id")]
        public string ProductId { get; set; } = string.Empty;
    }

    /// <summary>
    /// Buy/Sell details
    /// </summary>
    [SerializationModel]
    public record CoinbaseBuyOrSell
    {
        /// <summary>
        /// Fee details
        /// </summary>
        [JsonPropertyName("fee")]
        public CoinbaseQuantityReference? Fee { get; set; } = null!;
        /// <summary>
        /// Id of the buy/sell
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Payment method name
        /// </summary>
        [JsonPropertyName("payment_method_name")]
        public string PaymentMethodName { get; set; } = string.Empty;
        /// <summary>
        /// Subtotal info
        /// </summary>
        [JsonPropertyName("subtotal")]
        public CoinbaseQuantityReference Subtotal { get; set; } = null!;
        /// <summary>
        /// Total info
        /// </summary>
        [JsonPropertyName("total")]
        public CoinbaseQuantityReference Total { get; set; } = null!;
    }
}
