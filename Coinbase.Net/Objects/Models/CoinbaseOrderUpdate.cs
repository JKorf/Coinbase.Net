using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Order update
    /// </summary>
    [SerializationModel]
    public record CoinbaseOrderUpdate
    {
        /// <summary>
        /// ["<c>avg_price</c>"] Average price
        /// </summary>
        [JsonPropertyName("avg_price")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// ["<c>cancel_reason</c>"] Cancel reason
        /// </summary>
        [JsonPropertyName("cancel_reason")]
        public string? CancelReason { get; set; }
        /// <summary>
        /// ["<c>client_order_id</c>"] Client order id
        /// </summary>
        [JsonPropertyName("client_order_id")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>completion_percentage</c>"] Completion percentage
        /// </summary>
        [JsonPropertyName("completion_percentage")]
        public decimal CompletionPercentage { get; set; }
        /// <summary>
        /// ["<c>contract_expiry_type</c>"] Contract expiry type
        /// </summary>
        [JsonPropertyName("contract_expiry_type")]
        public ContractExpiryType? ContractExpiryType { get; set; }
        /// <summary>
        /// ["<c>cumulative_quantity</c>"] Cumulative quantity filled
        /// </summary>
        [JsonPropertyName("cumulative_quantity")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>filled_value</c>"] Filled value
        /// </summary>
        [JsonPropertyName("filled_value")]
        public decimal ValueFilled { get; set; }
        /// <summary>
        /// ["<c>leaves_quantity</c>"] Remaining quantity
        /// </summary>
        [JsonPropertyName("leaves_quantity")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// ["<c>limit_price</c>"] Limit price
        /// </summary>
        [JsonPropertyName("limit_price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>number_of_fills</c>"] Number of trades for the order
        /// </summary>
        [JsonPropertyName("number_of_fills")]
        public int NumberOfTrades { get; set; }
        /// <summary>
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>order_side</c>"] Order side
        /// </summary>
        [JsonPropertyName("order_side")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// ["<c>order_type</c>"] Order type
        /// </summary>
        [JsonPropertyName("order_type")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>outstanding_hold_amount</c>"] Outstanding hold quantity
        /// </summary>
        [JsonPropertyName("outstanding_hold_amount")]
        public decimal OutstandingHoldQuantity { get; set; }
        /// <summary>
        /// ["<c>post_only</c>"] Post only
        /// </summary>
        [JsonPropertyName("post_only")]
        public bool? PostOnly { get; set; }
        /// <summary>
        /// ["<c>product_id</c>"] Symbol
        /// </summary>
        [JsonPropertyName("product_id")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>product_type</c>"] Product type
        /// </summary>
        [JsonPropertyName("product_type")]
        public SymbolType SymbolType { get; set; }
        /// <summary>
        /// ["<c>reject_reason</c>"] Reject reason
        /// </summary>
        [JsonPropertyName("reject_reason")]
        public string? RejectReason { get; set; }
        /// <summary>
        /// ["<c>retail_portfolio_id</c>"] Retail portfolio id
        /// </summary>
        [JsonPropertyName("retail_portfolio_id")]
        public string RetailPortfolioId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>risk_managed_by</c>"] Risk managed by
        /// </summary>
        [JsonPropertyName("risk_managed_by")]
        public RiskManageType? RiskManagedBy { get; set; }
        /// <summary>
        /// ["<c>status</c>"] Order status
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>stop_price</c>"] Stop price
        /// </summary>
        [JsonPropertyName("stop_price")]
        public decimal? StopPrice { get; set; }
        /// <summary>
        /// ["<c>time_in_force</c>"] Time in force
        /// </summary>
        [JsonPropertyName("time_in_force")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// ["<c>total_fees</c>"] Fees paid for the order
        /// </summary>
        [JsonPropertyName("total_fees")]
        public decimal TotalFees { get; set; }
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
        /// ["<c>creation_time</c>"] Order placement time
        /// </summary>
        [JsonPropertyName("creation_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>end_time</c>"] Order finished time
        /// </summary>
        [JsonPropertyName("end_time")]
        public DateTime? FinishTime { get; set; }
        /// <summary>
        /// ["<c>start_time</c>"] Order start time
        /// </summary>
        [JsonPropertyName("start_time")]
        public DateTime? StartTime { get; set; }
    }
}
