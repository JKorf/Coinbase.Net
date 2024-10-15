using Coinbase.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase.Net.Objects.Models
{
    internal record CoinbaseBookTickerWrapper
    {
        [JsonPropertyName("pricebooks")]
        public IEnumerable<CoinbaseBookTicker> Data { get; set; } = null!;
    }

    /// <summary>
    /// Book ticker
    /// </summary>
    public record CoinbaseBookTicker
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("product_id")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Asks
        /// </summary>
        [JsonInclude, JsonPropertyName("asks")]
        internal CoinbaseOrderBookEntry[] Asks { get; set; } = [];
        /// <summary>
        /// Bids
        /// </summary>
        [JsonInclude, JsonPropertyName("bids")]
        internal CoinbaseOrderBookEntry[] Bids { get; set; } = [];
        /// <summary>
        /// Best ask price
        /// </summary>
        public decimal BestAskPrice => Asks[0].Price;
        /// <summary>
        /// Best ask quantity
        /// </summary>
        public decimal BestAskQuantity => Asks[0].Quantity;
        /// <summary>
        /// Best bid price
        /// </summary>
        public decimal BestBidPrice => Bids[0].Price;
        /// <summary>
        /// Best bid quantity
        /// </summary>
        public decimal BestBidQuantity => Bids[0].Quantity;
    }


}
