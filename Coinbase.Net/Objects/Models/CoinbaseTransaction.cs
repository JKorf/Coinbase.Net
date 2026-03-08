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
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>type</c>"] Type of the transaction
        /// </summary>
        [JsonPropertyName("type")]
        public TransactionType TransactionType { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Status of the transaction
        /// </summary>
        [JsonPropertyName("status")]
        public TransactionStatus TransactionStatus { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Quantity
        /// </summary>
        [JsonPropertyName("amount")]
        public CoinbaseQuantityReference Quantity { get; set; } = null!;
        /// <summary>
        /// ["<c>native_amount</c>"] Native quantity
        /// </summary>
        [JsonPropertyName("native_amount")]
        public CoinbaseQuantityReference NativeQuantity { get; set; } = null!;
        /// <summary>
        /// ["<c>description</c>"] Description
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }
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
        /// ["<c>network</c>"] Network info
        /// </summary>
        [JsonPropertyName("network")]
        public CoinbaseTransactionNetwork? Network { get; set; }
        /// <summary>
        /// ["<c>to</c>"] Transfer target
        /// </summary>
        [JsonPropertyName("to")]
        public CoinbaseToReference? To { get; set; }
        /// <summary>
        /// ["<c>from</c>"] Transfer source
        /// </summary>
        [JsonPropertyName("from")]
        public CoinbaseToReference? From { get; set; }
        /// <summary>
        /// ["<c>details</c>"] Details
        /// </summary>
        [JsonPropertyName("details")]
        public CoinbaseTransactionDetails Details { get; set; } = null!;
        /// <summary>
        /// ["<c>trade</c>"] Trade info
        /// </summary>
        [JsonPropertyName("trade")]
        public CoinbaseTransactionTrade? Trade { get; set; }
        /// <summary>
        /// ["<c>buy</c>"] Buy info
        /// </summary>
        [JsonPropertyName("buy")]
        public CoinbaseBuyOrSell? Buy { get; set; }
        /// <summary>
        /// ["<c>sell</c>"] Sell info
        /// </summary>
        [JsonPropertyName("sell")]
        public CoinbaseBuyOrSell? Sell { get; set; }
        /// <summary>
        /// ["<c>advanced_trade_fill</c>"] Advanced trade fill info
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
        /// ["<c>status</c>"] Status
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>hash</c>"] Transaction hash, only provided when this is a SEND transaction
        /// </summary>
        [JsonPropertyName("hash")]
        public string? Hash { get; set; }
        /// <summary>
        /// ["<c>name</c>"] Name of the network, only provided when this is a SEND transaction
        /// </summary>
        [JsonPropertyName("name")]
        public string? NetworkName { get; set; }

        /// <summary>
        /// ["<c>transaction_fee</c>"] Transaction fee, only provided when this is a SEND transaction
        /// </summary>
        [JsonPropertyName("transaction_fee")]
        public CoinbaseQuantityReference? TransactionFee { get; set; }

        /// <summary>
        /// ["<c>transaction_amount</c>"] Transaction quantity, only provided when this is a SEND transaction
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
        /// ["<c>title</c>"] Title
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>subtitle</c>"] Subtitle
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
        /// ["<c>id</c>"] Id of the trade
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>payment_method_name</c>"] The name of the payment method used for the trade
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
        /// ["<c>commission</c>"] Commission
        /// </summary>
        [JsonPropertyName("commission")]
        public decimal Commission { get; set; }
        /// <summary>
        /// ["<c>fill_price</c>"] Fill price
        /// </summary>
        [JsonPropertyName("fill_price")]
        public decimal FillPrice { get; set; }
        /// <summary>
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>order_side</c>"] Order side (buy/sell)
        /// </summary>
        [JsonPropertyName("order_side")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// ["<c>product_id</c>"] Product id
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
        /// ["<c>fee</c>"] Fee details
        /// </summary>
        [JsonPropertyName("fee")]
        public CoinbaseQuantityReference? Fee { get; set; } = null!;
        /// <summary>
        /// ["<c>id</c>"] Id of the buy/sell
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>payment_method_name</c>"] Payment method name
        /// </summary>
        [JsonPropertyName("payment_method_name")]
        public string PaymentMethodName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>subtotal</c>"] Subtotal info
        /// </summary>
        [JsonPropertyName("subtotal")]
        public CoinbaseQuantityReference Subtotal { get; set; } = null!;
        /// <summary>
        /// ["<c>total</c>"] Total info
        /// </summary>
        [JsonPropertyName("total")]
        public CoinbaseQuantityReference Total { get; set; } = null!;
    }
}
