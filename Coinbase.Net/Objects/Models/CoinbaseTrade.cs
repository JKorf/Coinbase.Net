using CryptoExchange.Net.Converters.SystemTextJson;
using Coinbase.Net.Enums;
using System;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Trade info
    /// </summary>
    [SerializationModel]
    public record CoinbaseTrades
    {
        /// <summary>
        /// ["<c>trades</c>"] Trades
        /// </summary>
        [JsonPropertyName("trades")]
        public CoinbaseTrade[] Trades { get; set; } = Array.Empty<CoinbaseTrade>();
        /// <summary>
        /// ["<c>best_bid</c>"] Best bid price
        /// </summary>
        [JsonPropertyName("best_bid")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>best_ask</c>"] Best ask price
        /// </summary>
        [JsonPropertyName("best_ask")]
        public decimal BestAskPrice { get; set; }
    }

    /// <summary>
    /// Trade info
    /// </summary>
    [SerializationModel]
    public record CoinbaseTrade
    {
        /// <summary>
        /// ["<c>time</c>"] Time of the trade
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>trade_id</c>"] Trade id
        /// </summary>
        [JsonPropertyName("trade_id")]
        public string TradeId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>product_id</c>"] Symbol
        /// </summary>
        [JsonPropertyName("product_id")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>price</c>"] Trade price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>size</c>"] Quantity traded
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>side</c>"] Side of the trade
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide OrderSide { get; set; }
    }


}
