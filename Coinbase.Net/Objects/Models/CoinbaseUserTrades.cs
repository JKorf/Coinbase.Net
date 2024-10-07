using Coinbase.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// User trades
    /// </summary>
    public record CoinbaseUserTrades
    {
        /// <summary>
        /// Trade list
        /// </summary>
        [JsonPropertyName("fills")]
        public IEnumerable<CoinbaseUserTrade> Trades { get; set; } = null!;
        /// <summary>
        /// Next page cursor
        /// </summary>
        [JsonPropertyName("cursor")]
        public string Cursor { get; set; } = string.Empty;
    }

    /// <summary>
    /// User trade info
    /// </summary>
    public record CoinbaseUserTrade
    {
        /// <summary>
        /// Entry id
        /// </summary>
        [JsonPropertyName("entry_id")]
        public string EntryId { get; set; } = string.Empty;
        /// <summary>
        /// Trade id
        /// </summary>
        [JsonPropertyName("trade_id")]
        public string TradeId { get; set; } = string.Empty;
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Trade time
        /// </summary>
        [JsonPropertyName("trade_time")]
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// Trade type
        /// </summary>
        [JsonPropertyName("trade_type")]
        public string TradeType { get; set; } = string.Empty;
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
        /// Fee paid
        /// </summary>
        [JsonPropertyName("commission")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("product_id")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Time at which this fill was posted.
        /// </summary>
        [JsonPropertyName("sequence_timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Trade role
        /// </summary>
        [JsonPropertyName("liquidity_indicator")]
        public TradeRole TradeRole { get; set; }
        /// <summary>
        /// Whether the order was placed with quote asset
        /// </summary>
        [JsonPropertyName("size_in_quote")]
        public bool QuantityInQuoteAsset { get; set; }
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("user_id")]
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// Side of the order
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// Retail portfolio id
        /// </summary>
        [JsonPropertyName("retail_portfolio_id")]
        public string RetailPortfolioId { get; set; } = string.Empty;
    }


}
