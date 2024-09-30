using Coinbase.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    /// <summary>
    /// Trade info
    /// </summary>
    public record CoinbaseTrades
    {
        /// <summary>
        /// Trades
        /// </summary>
        [JsonPropertyName("trades")]
        public IEnumerable<CoinbaseTrade> Trades { get; set; } = Array.Empty<CoinbaseTrade>();
        /// <summary>
        /// Best bid price
        /// </summary>
        [JsonPropertyName("best_bid")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// Best ask price
        /// </summary>
        [JsonPropertyName("best_ask")]
        public decimal BestAskPrice { get; set; }
    }

    /// <summary>
    /// Trade info
    /// </summary>
    public record CoinbaseTrade
    {
        /// <summary>
        /// Time of the trade
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Trade id
        /// </summary>
        [JsonPropertyName("trade_id")]
        public long TradeId { get; set; }
        /// <summary>
        /// Trade price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity traded
        /// </summary>
        [JsonPropertyName("size")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Side of the trade
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide OrderSide { get; set; }
    }


}
