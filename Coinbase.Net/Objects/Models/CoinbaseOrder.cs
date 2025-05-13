using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    [SerializationModel]
    internal record CoinbaseOrderWrapper
    {
        /// <summary>
        /// Order
        /// </summary>
        [JsonPropertyName("order")]
        public CoinbaseOrder Order { get; set; } = null!;
    }

    [SerializationModel]
    internal record CoinbaseOrdersWrapper
    {
        /// <summary>
        /// Orders
        /// </summary>
        [JsonPropertyName("orders")]
        public CoinbaseOrder[] Orders { get; set; } = null!;
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    public record CoinbaseOrder
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("product_id")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("user_id")]
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// Order configuration
        /// </summary>
        [JsonPropertyName("order_configuration")]
        public CoinbaseOrderConfiguration OrderConfiguration { get; set; } = null!;
        /// <summary>
        /// OrderSide
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide? OrderSide { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("client_order_id")]
        public string ClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// Status of the order
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatus OrderStatus { get; set; }
        /// <summary>
        /// Time in force
        /// </summary>
        [JsonPropertyName("time_in_force")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// Created time
        /// </summary>
        [JsonPropertyName("created_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Filled percentage of the order
        /// </summary>
        [JsonPropertyName("completion_percentage")]
        public decimal FilledPercentage { get; set; }
        /// <summary>
        /// Quantity filled
        /// </summary>
        [JsonPropertyName("filled_size")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// Average fill price
        /// </summary>
        [JsonPropertyName("average_filled_price")]
        public decimal AverageFillPrice { get; set; }
        /// <summary>
        /// Number of trades
        /// </summary>
        [JsonPropertyName("number_of_fills")]
        public decimal NumberOfTrades { get; set; }
        /// <summary>
        /// Filled quote quantity value
        /// </summary>
        [JsonPropertyName("filled_value")]
        public decimal QuoteQuantityFilled { get; set; }
        /// <summary>
        /// Pending cancel
        /// </summary>
        [JsonPropertyName("pending_cancel")]
        public bool PendingCancel { get; set; }
        /// <summary>
        /// Whether the order quantity was in quote asset
        /// </summary>
        [JsonPropertyName("size_in_quote")]
        public bool QuantityInQuoteAsset { get; set; }
        /// <summary>
        /// Total fees for the order
        /// </summary>
        [JsonPropertyName("total_fees")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Whether fees are included in the quantity
        /// </summary>
        [JsonPropertyName("size_inclusive_of_fees")]
        public bool QuantityInclusiveOfFees { get; set; }
        /// <summary>
        /// Total value after fees
        /// </summary>
        [JsonPropertyName("total_value_after_fees")]
        public decimal TotalValueAfterFees { get; set; }
        /// <summary>
        /// Trigger status
        /// </summary>
        [JsonPropertyName("trigger_status")]
        public TriggerStatus? TriggerStatus { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("order_type")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// Reject reason
        /// </summary>
        [JsonPropertyName("reject_reason")]
        public string? RejectReason { get; set; }
        /// <summary>
        /// Settled
        /// </summary>
        [JsonPropertyName("settled")]
        public bool Settled { get; set; }
        /// <summary>
        /// Symbol type
        /// </summary>
        [JsonPropertyName("product_type")]
        public SymbolType SymbolType { get; set; }
        /// <summary>
        /// Reject message
        /// </summary>
        [JsonPropertyName("reject_message")]
        public string? RejectMessage { get; set; }
        /// <summary>
        /// Cancel message
        /// </summary>
        [JsonPropertyName("cancel_message")]
        public string? CancelMessage { get; set; }
        /// <summary>
        /// Order placement source
        /// </summary>
        [JsonPropertyName("order_placement_source")]
        public string OrderPlacementSource { get; set; } = string.Empty;
        /// <summary>
        /// Outstanding hold quantity
        /// </summary>
        [JsonPropertyName("outstanding_hold_amount")]
        public decimal OutstandingHoldQuantity { get; set; }
        /// <summary>
        /// Is liquidation
        /// </summary>
        [JsonPropertyName("is_liquidation")]
        public bool? IsLiquidation { get; set; }
        /// <summary>
        /// Last fill time
        /// </summary>
        [JsonPropertyName("last_fill_time")]
        public DateTime? LastFillTime { get; set; }
        /// <summary>
        /// Edit history
        /// </summary>
        [JsonPropertyName("edit_history")]
        public CoinbaseOrderEditHistory[]? EditHistory { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal? Leverage { get; set; }
        /// <summary>
        /// Margin type
        /// </summary>
        [JsonPropertyName("margin_type")]
        public MarginType? MarginType { get; set; }
        /// <summary>
        /// Retail portfolio id
        /// </summary>
        [JsonPropertyName("retail_portfolio_id")]
        public string? RetailPortfolioId { get; set; }
    }

    /// <summary>
    /// Edit history
    /// </summary>
    [SerializationModel]
    public record CoinbaseOrderEditHistory
    {
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Replace accept timestamp
        /// </summary>
        [JsonPropertyName("replace_accept_timestamp")]
        public DateTime ReplaceAcceptTimestamp { get; set; }
    }


}
