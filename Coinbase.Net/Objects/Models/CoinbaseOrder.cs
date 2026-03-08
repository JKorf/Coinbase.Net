using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    [SerializationModel]
    internal record CoinbaseOrderWrapper
    {
        /// <summary>
        /// ["<c>order</c>"] Order
        /// </summary>
        [JsonPropertyName("order")]
        public CoinbaseOrder Order { get; set; } = null!;
    }

    [SerializationModel]
    internal record CoinbaseOrdersWrapper
    {
        /// <summary>
        /// ["<c>orders</c>"] Orders
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
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>product_id</c>"] Symbol
        /// </summary>
        [JsonPropertyName("product_id")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>user_id</c>"] User id
        /// </summary>
        [JsonPropertyName("user_id")]
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>order_configuration</c>"] Order configuration
        /// </summary>
        [JsonPropertyName("order_configuration")]
        public CoinbaseOrderConfiguration OrderConfiguration { get; set; } = null!;
        /// <summary>
        /// ["<c>side</c>"] OrderSide
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide? OrderSide { get; set; }
        /// <summary>
        /// ["<c>client_order_id</c>"] Client order id
        /// </summary>
        [JsonPropertyName("client_order_id")]
        public string ClientOrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>status</c>"] Status of the order
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatus OrderStatus { get; set; }
        /// <summary>
        /// ["<c>time_in_force</c>"] Time in force
        /// </summary>
        [JsonPropertyName("time_in_force")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// ["<c>created_time</c>"] Created time
        /// </summary>
        [JsonPropertyName("created_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>completion_percentage</c>"] Filled percentage of the order
        /// </summary>
        [JsonPropertyName("completion_percentage")]
        public decimal FilledPercentage { get; set; }
        /// <summary>
        /// ["<c>filled_size</c>"] Quantity filled
        /// </summary>
        [JsonPropertyName("filled_size")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>average_filled_price</c>"] Average fill price
        /// </summary>
        [JsonPropertyName("average_filled_price")]
        public decimal AverageFillPrice { get; set; }
        /// <summary>
        /// ["<c>number_of_fills</c>"] Number of trades
        /// </summary>
        [JsonPropertyName("number_of_fills")]
        public decimal NumberOfTrades { get; set; }
        /// <summary>
        /// ["<c>filled_value</c>"] Filled quote quantity value
        /// </summary>
        [JsonPropertyName("filled_value")]
        public decimal QuoteQuantityFilled { get; set; }
        /// <summary>
        /// ["<c>pending_cancel</c>"] Pending cancel
        /// </summary>
        [JsonPropertyName("pending_cancel")]
        public bool PendingCancel { get; set; }
        /// <summary>
        /// ["<c>size_in_quote</c>"] Whether the order quantity was in quote asset
        /// </summary>
        [JsonPropertyName("size_in_quote")]
        public bool QuantityInQuoteAsset { get; set; }
        /// <summary>
        /// ["<c>total_fees</c>"] Total fees for the order
        /// </summary>
        [JsonPropertyName("total_fees")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>size_inclusive_of_fees</c>"] Whether fees are included in the quantity
        /// </summary>
        [JsonPropertyName("size_inclusive_of_fees")]
        public bool QuantityInclusiveOfFees { get; set; }
        /// <summary>
        /// ["<c>total_value_after_fees</c>"] Total value after fees
        /// </summary>
        [JsonPropertyName("total_value_after_fees")]
        public decimal TotalValueAfterFees { get; set; }
        /// <summary>
        /// ["<c>trigger_status</c>"] Trigger status
        /// </summary>
        [JsonPropertyName("trigger_status")]
        public TriggerStatus? TriggerStatus { get; set; }
        /// <summary>
        /// ["<c>order_type</c>"] Order type
        /// </summary>
        [JsonPropertyName("order_type")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>reject_reason</c>"] Reject reason
        /// </summary>
        [JsonPropertyName("reject_reason")]
        public string? RejectReason { get; set; }
        /// <summary>
        /// ["<c>settled</c>"] Settled
        /// </summary>
        [JsonPropertyName("settled")]
        public bool Settled { get; set; }
        /// <summary>
        /// ["<c>product_type</c>"] Symbol type
        /// </summary>
        [JsonPropertyName("product_type")]
        public SymbolType SymbolType { get; set; }
        /// <summary>
        /// ["<c>reject_message</c>"] Reject message
        /// </summary>
        [JsonPropertyName("reject_message")]
        public string? RejectMessage { get; set; }
        /// <summary>
        /// ["<c>cancel_message</c>"] Cancel message
        /// </summary>
        [JsonPropertyName("cancel_message")]
        public string? CancelMessage { get; set; }
        /// <summary>
        /// ["<c>order_placement_source</c>"] Order placement source
        /// </summary>
        [JsonPropertyName("order_placement_source")]
        public string OrderPlacementSource { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>outstanding_hold_amount</c>"] Outstanding hold quantity
        /// </summary>
        [JsonPropertyName("outstanding_hold_amount")]
        public decimal OutstandingHoldQuantity { get; set; }
        /// <summary>
        /// ["<c>is_liquidation</c>"] Is liquidation
        /// </summary>
        [JsonPropertyName("is_liquidation")]
        public bool? IsLiquidation { get; set; }
        /// <summary>
        /// ["<c>last_fill_time</c>"] Last fill time
        /// </summary>
        [JsonPropertyName("last_fill_time")]
        public DateTime? LastFillTime { get; set; }
        /// <summary>
        /// ["<c>edit_history</c>"] Edit history
        /// </summary>
        [JsonPropertyName("edit_history")]
        public CoinbaseOrderEditHistory[]? EditHistory { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal? Leverage { get; set; }
        /// <summary>
        /// ["<c>margin_type</c>"] Margin type
        /// </summary>
        [JsonPropertyName("margin_type")]
        public MarginType? MarginType { get; set; }
        /// <summary>
        /// ["<c>retail_portfolio_id</c>"] Retail portfolio id
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
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>size</c>"] Quantity
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>replace_accept_timestamp</c>"] Replace accept timestamp
        /// </summary>
        [JsonPropertyName("replace_accept_timestamp")]
        public DateTime ReplaceAcceptTimestamp { get; set; }
    }


}
