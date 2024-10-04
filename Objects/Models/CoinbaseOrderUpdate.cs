using Coinbase.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Order update
    /// </summary>
    public record CoinbaseOrderUpdate
    {
        /// <summary>
        /// Average price
        /// </summary>
        [JsonPropertyName("avg_price")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// Cancel reason
        /// </summary>
        [JsonPropertyName("cancel_reason")]
        public string? CancelReason { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("client_order_id")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Completion percentage
        /// </summary>
        [JsonPropertyName("completion_percentage")]
        public decimal CompletionPercentage { get; set; }
        /// <summary>
        /// Contract expiry type
        /// </summary>
        [JsonPropertyName("contract_expiry_type")]
        public ContractExpiryType? ContractExpiryType { get; set; }
        /// <summary>
        /// Cumulative quantity filled
        /// </summary>
        [JsonPropertyName("cumulative_quantity")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// Filled value
        /// </summary>
        [JsonPropertyName("filled_value")]
        public decimal ValueFilled { get; set; }
        /// <summary>
        /// Remaining quantity
        /// </summary>
        [JsonPropertyName("leaves_quantity")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// Limit price
        /// </summary>
        [JsonPropertyName("limit_price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// Number of trades for the order
        /// </summary>
        [JsonPropertyName("number_of_fills")]
        public int NumberOfTrades { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("order_side")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("order_type")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// Outstanding hold quantity
        /// </summary>
        [JsonPropertyName("outstanding_hold_amount")]
        public decimal OutstandingHoldQuantity { get; set; }
        /// <summary>
        /// Post only
        /// </summary>
        [JsonPropertyName("post_only")]
        public bool PostOnly { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("product_id")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Product type
        /// </summary>
        [JsonPropertyName("product_type")]
        public SymbolType SymbolType { get; set; }
        /// <summary>
        /// Reject reason
        /// </summary>
        [JsonPropertyName("reject_reason")]
        public string? RejectReason { get; set; }
        /// <summary>
        /// Retail portfolio id
        /// </summary>
        [JsonPropertyName("retail_portfolio_id")]
        public string RetailPortfolioId { get; set; } = string.Empty;
        /// <summary>
        /// Risk managed by
        /// </summary>
        [JsonPropertyName("risk_managed_by")]
        public RiskManageType? RiskManagedBy { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// Stop price
        /// </summary>
        [JsonPropertyName("stop_price")]
        public decimal? StopPrice { get; set; }
        /// <summary>
        /// Time in force
        /// </summary>
        [JsonPropertyName("time_in_force")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// Fees paid for the order
        /// </summary>
        [JsonPropertyName("total_fees")]
        public decimal TotalFees { get; set; }
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
        /// Order placement time
        /// </summary>
        [JsonPropertyName("creation_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Order finished time
        /// </summary>
        [JsonPropertyName("end_time")]
        public DateTime? FinishTime { get; set; }
        /// <summary>
        /// Order start time
        /// </summary>
        [JsonPropertyName("start_time")]
        public DateTime? StartTime { get; set; }
    }
}
