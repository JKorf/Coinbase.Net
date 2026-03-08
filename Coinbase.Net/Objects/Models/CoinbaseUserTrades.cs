using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// User trades
    /// </summary>
    [SerializationModel]
    public record CoinbaseUserTrades
    {
        /// <summary>
        /// ["<c>fills</c>"] Trade list
        /// </summary>
        [JsonPropertyName("fills")]
        public CoinbaseUserTrade[] Trades { get; set; } = null!;
        /// <summary>
        /// ["<c>cursor</c>"] Next page cursor
        /// </summary>
        [JsonPropertyName("cursor")]
        public string Cursor { get; set; } = string.Empty;
    }

    /// <summary>
    /// User trade info
    /// </summary>
    [SerializationModel]
    public record CoinbaseUserTrade
    {
        /// <summary>
        /// ["<c>entry_id</c>"] Entry id
        /// </summary>
        [JsonPropertyName("entry_id")]
        public string EntryId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>trade_id</c>"] Trade id
        /// </summary>
        [JsonPropertyName("trade_id")]
        public string TradeId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>trade_time</c>"] Trade time
        /// </summary>
        [JsonPropertyName("trade_time")]
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// ["<c>trade_type</c>"] Trade type
        /// </summary>
        [JsonPropertyName("trade_type")]
        public string TradeType { get; set; } = string.Empty;
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
        /// ["<c>commission</c>"] Fee paid
        /// </summary>
        [JsonPropertyName("commission")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>product_id</c>"] Symbol
        /// </summary>
        [JsonPropertyName("product_id")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>sequence_timestamp</c>"] Time at which this fill was posted.
        /// </summary>
        [JsonPropertyName("sequence_timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>liquidity_indicator</c>"] Trade role
        /// </summary>
        [JsonPropertyName("liquidity_indicator")]
        public TradeRole TradeRole { get; set; }
        /// <summary>
        /// ["<c>size_in_quote</c>"] Whether the order was placed with quote asset
        /// </summary>
        [JsonPropertyName("size_in_quote")]
        public bool QuantityInQuoteAsset { get; set; }
        /// <summary>
        /// ["<c>user_id</c>"] User id
        /// </summary>
        [JsonPropertyName("user_id")]
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>side</c>"] Side of the order
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// ["<c>retail_portfolio_id</c>"] Retail portfolio id
        /// </summary>
        [JsonPropertyName("retail_portfolio_id")]
        public string RetailPortfolioId { get; set; } = string.Empty;
    }


}
